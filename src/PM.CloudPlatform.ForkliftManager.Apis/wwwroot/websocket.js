var url = "ws://39.100.144.78:9002"
//var url = "ws://localhost:28081"
//import conf from './js/conf.js'
//var url = conf.ws_url;

var userId;
var isGps = false;
// $("#input input").val("868120278343194")
ajax("get", "api/User/GetLoginUserInfo")
    .then(function (res) {
        if (res.code == 200) {
            userinfo = res.data
            userId = userinfo.id
        }
    });
let markerData = {}
var RadioVal;
// let userId =
var VerifyCode;
var vehicleData;
webSocketInit(url)
function webSocketInit(url) {
    ws = new WebSocket(url);
    // 打开事件
    let o = { //登录obj
        PackageType: 1,
        ClientId: userId,
    }

    ws.onopen = function (evt) {
        ws.send(JSON.stringify(o)); //开启人员实时位置推送，只会推送当前登录账号所属部门下的人员位置
        // ws.send("start msg"); //开启信息推送推送，开启后会接受所有为接受的信息
        // ws.send("start web alarm"); //开启预计信息推送
        timedCount() //每过一分钟发一次信息，保证ws连接
    };
    //接受信息
    ws.onmessage = function (evt) {
        switch (JSON.parse(evt.data).PackageType) {
            case 1: //登录
                VerifyCode = JSON.parse(evt.data).VerifyCode;
                break;
            case 2: //Gps定位包
                isGps = true;
                data = {
                    TerminalId: RadioVal
                }
                console.log(JSON.parse(evt.data))
                var datas;

                if (!!RadioVal) {
                    let x = JSON.parse(evt.data).Data;
                    if (x.TerminalId == RadioVal) {
                        markerData = {};
                        map.clearMap();
                        datas = x;
                    }
                    // else{
                    //     console.log(1)
                    //     datas = JSON.parse(evt.data).Data;
                    // }
                } else {
                    datas = JSON.parse(evt.data).Data;
                }
                var icon = new AMap.Icon({
                    size: new AMap.Size(40, 40),    // 图标尺寸
                    image: './images/fk_car.png',  // Icon的图像 images/011-叉车.png
                    // imageOffset: new AMap.Pixel(0, -60),  // 图像相对展示区域的偏移量，适于雪碧图等
                    imageSize: new AMap.Size(40, 40)   // 根据所设置的大小拉伸或压缩图片
                });
                var TerminalId = datas.TerminalId
                marker = new AMap.Marker({
                    icon: icon,
                    size: new AMap.Size(40, 40),
                    position: [datas.Lon, datas.Lat],
                    ExtData: datas,
                    draggable: true,
                    cursor: 'move',
                    // 设置拖拽效果
                    raiseOnDrag: false,
                });
                marker.on('click', function (e) {
                    let data = e.target.De.ExtData;
                    console.log(data)
                    vehicleData = data;
                    var content = "<div class='info-box'>";
                    content += "<h2>车辆信息</h2>";
                    content += '<div class="info-div">设备号:' + data.TerminalId + '</div>';
                    content += '<div class="info-div">车牌号:' + data.LicensePlateNumber + '</div>';
                    content += '<button type="button" id="track" class="layui-btn-fluid info-btn">轨迹回放</button>';
                    content += '<button type="button" id="SetFence" class="layui-btn-fluid info-btn">查看围栏</button>';
                    // content += '<button type="button" id="video" class="layui-btn layui-btn-fluid info-btn">车辆视频</button>';
                    content += '</div>';

                    // var content = "<div class='info-box'>";
                    // content += '<div class="info-div">名称：' + data.coveringName + '</div>';
                    // content += '<div class="info-div">地点：' + data.place + '</div>';
                    // content += '<div class="info-div">操作人：' + data.createBy + '</div>';
                    // content += '<div class="info-div">备注：' + data.remark + '</div></div>';
                    infoWindow.setContent(content);
                    infoWindow.open(map, e.target.getPosition());
                });
                if (markerData[TerminalId] != undefined) {
                    map.remove([markerData[TerminalId]]);
                }
                markerData[TerminalId] = marker;

                // let obj = {}
                // obj[TerminalId] = marker
                // for(let i = 0;i<markerData.length;i++){
                //     let keyArr = Object.keys(markerData[i])[0];
                //     console.log(keyArr)
                //     if(keyArr == TerminalId){
                //         map.remove([markerData[i][TerminalId]]);
                //         markerData.splice(i,1)
                //         i=i-1;
                //     }
                // }
                //console.log(markerData)
                // markerData.push(obj)
                // console.log(markerData)

                marker.setMap(map);

                break;
            case 3:

                let datass = JSON.parse(evt.data).Data;
                console.log(datass)
                var msgList = [

                ]
                var count = 0;
                var isQueue = false;
                setInterval(() => {
                    msgList.push(`<div>` + datass.LicensePlateNumber + ` :: ` + datass.Msg + `</div>`)
                    var rand = random(0, msgList.length - 1);
                    if (count > msgList.length) {
                        count = 0;
                        isQueue = true;
                    }

                    if ($('#message-list').children().length < msgList.length / 2) {
                        isQueue = false;
                    }

                    if (isQueue) {
                        $('#message-list').append(msgList[rand]);
                        $('#message-list > div:nth-child(1)').remove();
                    } else {
                        $('#message-list').append(msgList[rand]);
                    }
                    count++;
                    var scrollHeight = $('#message-list').prop("scrollHeight")
                    $('#message-list').animate({ scrollTop: scrollHeight }, 'slow');
                }, 30 * 1000);
        }
    };
    ws.onclose = function () {
        reconnect(url);
    };
}

function reconnect(url) {
    // lockReconnect加锁，防止onclose、onerror两次重连
    // 进行重连
    setTimeout(function () {
        webSocketInit(url);
    }, 3000);
}
//关闭事件
ws.onclose = function (evt) {
    ws = new WebSocket(url);
    markers = []
};
function timedCount() {
    if (VerifyCode) {
        ws.send(JSON.stringify({
            PackageType: 0,
            VerifyCode: VerifyCode,
            ClientId: userId
        }))
        // ws.send(JSON.stringify({ //gps
        //     PackageType : 2,
        //     VerifyCode:VerifyCode,
        // }))
    }
    setTimeout("timedCount()", 3000);
    //每过一分钟运行一次
}
// websocket.onopen = function () {
//     var onSend = function (msg) {
//         websocket.send(msg);
//     }
//     onSend(JSON.stringify(data1));

//     console.log("开启成功");
//     var map = new AMap.Map('container', {
//         resizeEnable: true,
//         zoom: 10,
//         showIndoorMap: true,
//     });
//     //显示当前地图边界范围坐标
//     function logMapBounds() {
//         var bounds = map.getBounds();
//         document.querySelector("#ne").innerText = bounds.northeast.toString();
//         document.querySelector("#sw").innerText = bounds.southwest.toString();
//         var northeast = bounds.northeast;
//         var southwest = bounds.southwest;
//         return [northeast, southwest];
//     }
//     logMapBounds();

//     //绑定地图移动与缩放事件
//     map.on('moveend', function () {
//         let data = logMapBounds();
//         let datajson = {
//             OpCode: 2,
//             // 用户名
//             Username: "gaogang",
//             // 验证码, 到时候会有api请求, 暂时使用test写死了
//             VerifyCode: "test",
//             // 设备号的集合, 里面都是手机号或者设备号, 有api能获取所有的
//             // 如果是空, 那就是所有的设备
//             Identifies: [

//             ],
//             // 消息Id的集合, 设备的不同消息都有消息id, 通过这个可以获取
//             // 想要的数据, 如果是空的, 那就是所有的数据都接收
//             MsgIds: [

//             ],
//             TopLeftPoint: {
//                 Longitude: parseInt(data[0].lat),
//                 Latitude: parseInt(data[0].lng)
//             },
//             BottomRightPoint: {
//                 Longitude: parseInt(data[0].lat),
//                 Latitude: parseInt(data[0].lng)
//             }
//         }
//         onSend(JSON.stringify(datajson));
//     });
//     map.on('zoomend', function () {
//         var data = logMapBounds();
//         onSend(data);
//     });

//     // 将 icon 传入 marker

//     // 将 markers 添加到地图
//     websocket.onmessage = function (e) {
//         try {
//             var receiveData = JSON.parse(e.data);
//             endMarker.setPosition([receiveData.Bodies.Lng / 1000000, receiveData.Bodies.Lat / 1000000]); //更新点标记位置\
//             // console.log(e.data);
//         } catch {
//             console.log("-----------" + e.data)
//         }
//     };

// }