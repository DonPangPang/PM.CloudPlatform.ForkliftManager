/**
 * date:2021/02/03
 * author:Wang
 * description:此处公共参数与封装方法
 */


// 以下为公共参数
var requestUrl = 'http://localhost:10808/'; //请求地址公共前缀
var loginMark = localStorage.getItem('loginMark');   //loginMark
var userInfo = sessionStorage.getItem('userInfo');

var Token = JSON.parse(sessionStorage.getItem('Token'));
// //登录判断
// if(!userInfo && document.getElementsByTagName('title')[0].innerHTML != '后台管理-登陆'){
//     // window.location = '/layui/login.html';
// }

// 以下为封装方法
/**
 * ajax请求方法
 * @description 描述：基于Promise的jquery中ajax封装
 * @param {methods:String} 请求方式
 * @param {url:String} 请求地址
 * @param {param} 请求参数
 */
// var ajax = function (methods, url, param,dataType) {
//     return new Promise(function (resolve, reject) {
//         $.ajax({
//             type: methods,
//             url: requestUrl+url,
//             data: param,
// 			dataType: dataType || "json",
// 			async: true,
// 			cache: false,
//             success: function (data) {
//                 if(data.code == 200){
//                     resolve(data);
//                 }else if(data.code == 410){
//                     window.location = '/login.html';
//                 }
                
//             },
//             error: function (error) {
//                 reject(error)
//             }
//         });
//     }
//     )
// };

var ajax = function(methods, url, param, dataType) {
    let token = JSON.parse(sessionStorage.getItem('Token'))
    return new Promise(function(resolve, reject) {
        if(methods == "get" || methods == "Get"){
            $.ajax({
                type: methods||"get",
                url: requestUrl + url,
                data: param || {},
                // data: JSON.stringify(param) || {},
                dataType: dataType || "json",
                async: true,
                cache: false,
                beforeSend: function (request) {
                    request.setRequestHeader("Authorization","Bearer " + token);
                },
                headers: { 'Content-Type': 'application/json' },
                success: function(data) {
                    if (data) {
                        if (data.code == 200) {
                            resolve(data);
                        } else if (data.code == 401) {
                            layer.msg('授权过期!', {
                                time: 2000
                            },function () {
                                window.top.location.href = '/login.html';
                            });
                        } else if (data.code == 403) {
                            layer.msg('未分配权限,请联系管理员!', {
                                time: 2000
                            });
                        } else{
                            layer.msg('请求错误：错误状态' + data.code + '，错误信息：' + data.msg)
                        }
                    }
                },
                error: function(error) {
                    reject(error)
                }
            });
        }else{
            $.ajax({
                type: methods||"get",
                url: requestUrl + url,
                data: JSON.stringify(param) || {},
                dataType: dataType || "json",
                async: true,
                cache: false,
                beforeSend: function (request) {
                    request.setRequestHeader("Authorization","Bearer " + token);
                },
                headers: { 'Content-Type': 'application/json' },
                success: function(data) {
                    if (data) {
                        if (data.code == 200) {
                            resolve(data);
                        } else if (data.code == 401) {
                            layer.msg('授权过期!', {
                                time: 2000
                            },function () {
                                window.top.location.href = '/login.html';
                            });
                        } else if (data.code == 403) {
                            layer.msg('未分配权限,请联系管理员!', {
                                time: 2000
                            });
                        } else{
                            layer.msg('请求错误：错误状态' + data.code + '，错误信息：' + data.msg)
                        }
                    }
                },
                error: function(error) {
                    reject(error)
                }
            });
        }
    })
};
/**
 * @description 生成guid方法
 * @param {l:String} 连接方式，默认："_"
 * @return {h} 生成的guid
 */
function guid(l) {
	l = l || "_";
	var h = "";
	for (var j = 1; j <= 32; j++) {
		var k = Math.floor(Math.random() * 16).toString(16);
		h += k;
		if ((j == 8) || (j == 12) || (j == 16) || (j == 20)) {
			h += l
		}
	}
	return h
}


/**
 * @description 画出验证码的方法
 * @param {show_num：Array}，验证码的数字，例如：[2,2,2,2]
 */
function draw(show_num) {
    var canvas_width=$('#canvas').width();
    var canvas_height=$('#canvas').height();
    var canvas = document.getElementById("canvas");//获取到canvas的对象，演员
    var context = canvas.getContext("2d");//获取到canvas画图的环境，演员表演的舞台
    canvas.width = canvas_width;
    canvas.height = canvas_height;
    var sCode = "A,B,C,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,W,X,Y,Z,1,2,3,4,5,6,7,8,9,0";
    var aCode = sCode.split(",");
    var aLength = aCode.length;//获取到数组的长度
    for (var i = 0; i <= 3; i++) {
        var j = Math.floor(Math.random() * aLength);//获取到随机的索引值
        var deg = Math.random() * 30 * Math.PI / 180;//产生0~30之间的随机弧度
        var txt = show_num[i];  //若要随机，不接受show_num也可以生成，将此行注释，下面两行解注，参数show_num删掉即可
        // var txt = aCode[j];//得到随机的一个内容
        // show_num[i] = txt.toLowerCase();
        var x = 10 + i * 20;//文字在canvas上的x坐标
        var y = 20 + Math.random() * 8;//文字在canvas上的y坐标
        context.font = "bold 23px 微软雅黑";
        context.translate(x, y);
        context.rotate(deg);
        context.fillStyle = randomColor();
        context.fillText(txt, 0, 0);
        context.rotate(-deg);
        context.translate(-x, -y);
    }
    for (var i = 0; i <= 5; i++) { //验证码上显示线条
        context.strokeStyle = randomColor();
        context.beginPath();
        context.moveTo(Math.random() * canvas_width, Math.random() * canvas_height);
        context.lineTo(Math.random() * canvas_width, Math.random() * canvas_height);
        context.stroke();
    }
    for (var i = 0; i <= 30; i++) { //验证码上显示小点
        context.strokeStyle = randomColor();
        context.beginPath();
        var x = Math.random() * canvas_width;
        var y = Math.random() * canvas_height;
        context.moveTo(x, y);
        context.lineTo(x + 1, y + 1);
        context.stroke();
    }
}


/**
 * @description 得到随机的颜色值
 * @return rgb的颜色，例如rgb(255,159,123)
 */
function randomColor() {
    var r = Math.floor(Math.random() * 256);
    var g = Math.floor(Math.random() * 256);
    var b = Math.floor(Math.random() * 256);
    return "rgb(" + r + "," + g + "," + b + ")";
}


/**
 * @description 扩展数组去空方法
 */
Array.prototype.notempty = function(){
	let arr = [];
	this.map(function(v){
		if (v != undefined && v!=null && v!= 'empty') {
			arr.push(v)
		}
	})
	return arr
}
/**
 * @description 数组对象生成树结构数据
 * @param {textArr：Array} [数组对象],将要转化为树形数据的线型数据,必须项
 * @param {cid:String}子级自身的ID的关键值,必须项
 * @param {pid:String}父级自身的ID的关键值,必须项
 * @param {treeSpid:String}第一级的id,必须项
 * @param {children:Array}父级中子级的关键值,必须项
 * @param {obj:Object}想要添加的字段，key值为添加字段的key值，value值为添加字段的在现有数据中的值的key   例;{title:'F_FullName'},像每条数据中添加一个key为title的值，title是现有数据中的F_FullName值
 */
function renderTree(textArr,cid,pid,treeSpid,children,obj){
	textArr.map((v,i)=>{v[children] ? "" : textArr[i][children] = []});
	for (let i = 0; i < textArr.length; i++) {
		for (let j = 0; j < textArr.length; j++) {
			if (textArr[i][cid] == textArr[j][pid]) {
                if(obj){
                    for(let key  in obj){
                        textArr[j][key] = textArr[j][obj[key]]
                    }
                }
				textArr[i][children].push(textArr[j])
			}
        }
    };
	// textArr.map((v,i)=>{v.children.length == 0 ? delete textArr[i]:''});
	// textArr = textArr.notempty();
	let linarr = [];
	textArr.forEach(e=>{
        if(e[pid]==treeSpid){
            if(obj){
                for(let key  in obj){
                    e[key] = e[obj[key]]
                }
            }
            linarr.push(e)
        }
    });
	return linarr
	// return linarr.length == 1 ? linarr[0] : linarr
}


/**
 * @description 数组对象中树形数据修改key值
 * @param {org:Object}需要修改key值得数组对象,必须项
 * @param {ischeckedArr:Array} 已经勾选的数组，用来判断该条记录是否已经勾选，
 */
function mapTree (org,ischeckedArr){
    const haveChildren = Array.isArray(org.ChildNodes) && org.ChildNodes.length > 0;
    let isChecked;
    !ischeckedArr ? 
        isChecked = false :
            isChecked = ischeckedArr.indexOf(org.id) > -1 ?  true : false ;
    return {
        title: org.text,
        id: org.id,
        spread:true,
        checked:isChecked,
        // parentId:org.parentId,
        children: haveChildren ? org.ChildNodes.map(i => mapTree(i,ischeckedArr)) : []
    };
}


/**
 * @description 树状数据转线型数据
 * @param {arr:Array}需要转换的数组对象,必须项
 * @param {isColumn:String} 此字段表示是否提取每一项中的某一字段，非必须
 */
function extract(arr,isColumn){
    var link = [];
    var all = [];
    link = link.concat(arr);
    while(link.length){
        var first = link.shift();
        if(first.children){
            link = link.concat(first.children);
            delete first['children'];
        }
        isColumn ? all.push(first[isColumn]) :all.push(first);
    }
    return all;
}


/**
 * @description window.href携带参数，解析参数的方法
 * @return {obj:Object} 返回一个解析好的携带参数所组成的对象
 */
function hrefExtractOp(){
    let obj = {};
    let localUrl = window.location.search.substr(1).split('&');
    localUrl.map(function(v,i){
        let vlen = v.split('=');
        obj[vlen[0]] = decodeURIComponent(vlen[1]);
    })
    return obj;
}


/**
 * @description 主页面获取权限接口
 * @param  {url:String}主页面的页面地址，eg:'page/system/usermanagement.html'
 * @return {obj:Object} 返回一个对象，cols：[{},{}]表格表头属性，btns，[]表格按钮的事件key值
 */
function authorize(url){
    let info =JSON.parse(sessionStorage.getItem('userInfo')).baseinfo;
    let cols = [],obj = {};
    $.ajax({
        type: 'get',
        url: requestUrl+'Simple/Areas/PMSystemModule/Module/authorizebuttoncolumnlist',
        async:false,
        data: {
            token: info.token,
            loginMark:loginMark,
            data: JSON.stringify({url:url})
        },
        success: function (data) {
            console.log(data);
            if(data){
                if(data.code == 200){
                    // 字段权限处理
                    if(data.data && data.data.colss && data.data.colss.length > 0){
                        data.data.colss.forEach(function(v,i){
                            let colsItem = {};
                            colsItem.field = v.F_EnCode;
                            colsItem.title = v.F_FullName;
                            colsItem.width = v.F_Width;
                            colsItem.minWidth = v.F_MinWidth;
                            colsItem.hide = v.F_Hide;
                            colsItem.sort = v.F_Sort;
                            colsItem.align = v.F_Align;
                            colsItem.templet = v.F_Templet;
                            colsItem.toolbar = v.F_Toolbar;
                            cols[v.F_Description] = colsItem;
                        })
                    }
                    obj.cols = cols;
                    // 提取按钮key值
                    if(data.data.btns && data.data.btns != '{}'){
                        obj.btns = Object.keys(data.data.btns)
                    }
                }else if(data.code == 410){
                    layer.msg('登录过期，请从新登陆',function(){
                        window.location = '/login.html';
                    });
                }
            }
        },
        error: function (error) {
            layer.msg('权限加载失败，无法加载数据', {
                time: 4000
            });
        }
    });
    return obj;
}

/**
 * @description 下载
 * @return {options:Object}
 */
function down(options) {
    var defaults = {
        method: "GET",
        url: "",
        param: []
    };
    var options = $.extend(defaults, options);
    if (options.url && options.param) {
        var $form = $('<form action="' + options.url + '" method="' + (options.method || 'post') + '"></form>');
        for (var key in options.param) {
            var $input = $('<input type="hidden" data-back="backjk" />').attr('name', key).val(options.param[key]);
            $form.append($input);
        }
        $form.appendTo('body').submit().remove();
    };
}