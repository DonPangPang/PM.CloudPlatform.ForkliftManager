function echarts_5() {
    var chartDom = document.getElementById('echarts_5');
    var myChart = echarts.init(chartDom);
    var option;

    option = {
        // title: {
        //     text: '世界人口总量',
        //     subtext: '数据来自网络'
        // },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            },

        },
        legend: {
            data: ['2011年', '2012年']
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true,
            textStyle: {
                color: "#fff"
            }
        },
        xAxis: {
            type: 'value',
            boundaryGap: [0, 0.01],
            axisLine: {
                show: true,
                lineStyle: {
                    fontSize: 12,
                    color: 'rgb(255,255,255)',
                }
            },
        },
        yAxis: {
            type: 'category',
            data: ['巴西', '印尼', '美国', '印度', '中国', '世界(千万)'],
            axisLine: {
                show: true,
                lineStyle: {
                    color: 'rgb(255,255,255)',
                }
            },
        },
        series: [
            {
                name: '2011年',
                type: 'bar',
                data: [1.8, 2.3, 2.9, 10.4, 13.1, 63.0]
            },
            {
                name: '2012年',
                type: 'bar',
                data: [1.9, 2.3, 3.1, 12.1, 13.4, 68.1]
            }
        ]
    };

    option && myChart.setOption(option);
}

function echarts_6() {
    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('echarts_6'));

    var xData = function () {
        var data = ['NO.1', 'NO.2', 'NO.3', 'NO.4', 'NO.5'];

        return data;
    }();

    var data = [23, 22, 20, 30, 22]

    option = {
        // backgroundColor: "#141f56",

        tooltip: {
            show: "true",
            trigger: 'item',
            backgroundColor: 'rgba(0,0,0,0.4)', // 背景
            padding: [8, 10], //内边距
            // extraCssText: 'box-shadow: 0 0 3px rgba(255, 255, 255, 0.4);', //添加阴影
            formatter: function (params) {
                if (params.seriesName != "") {
                    return params.name + ' ：  ' + params.value + ' 辆';
                }
            },

        },
        grid: {
            borderWidth: 0,
            top: 20,
            bottom: 35,
            left: 40,
            right: 10,
            textStyle: {
                color: "#fff"
            }
        },
        xAxis: [{
            type: 'category',

            axisTick: {
                show: false
            },

            axisLine: {
                show: true,
                lineStyle: {
                    color: 'rgba(255,255,255,0.2)',
                }
            },
            axisLabel: {
                inside: false,
                textStyle: {
                    color: '#bac0c0',
                    fontWeight: 'normal',
                    fontSize: '12',
                },
                // formatter:function(val){
                //     return val.split("").join("\n")
                // },
            },
            data: xData,
        }, {
            type: 'category',
            axisLine: {
                show: false
            },
            axisTick: {
                show: false
            },
            axisLabel: {
                show: false
            },
            splitArea: {
                show: false
            },
            splitLine: {
                show: false
            },
            data: xData,
        }],
        yAxis: {
            min: 10,
            type: 'value',
            axisTick: {
                show: false
            },
            axisLine: {
                show: true,
                lineStyle: {
                    color: 'rgba(255,255,255,0.2)',
                }
            },
            splitLine: {
                show: true,
                lineStyle: {
                    color: 'rgba(255,255,255,0.1)',
                }
            },
            axisLabel: {
                textStyle: {
                    color: '#bac0c0',
                    fontWeight: 'normal',
                    fontSize: '12',
                },
                formatter: '{value}',
            },
        },
        series: [{
            type: 'bar',
            itemStyle: {
                normal: {
                    show: true,
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: '#00c0e9'
                    }, {
                        offset: 1,
                        color: '#3b73cf'
                    }]),
                    barBorderRadius: 50,
                    borderWidth: 0,
                },
                emphasis: {
                    shadowBlur: 15,
                    shadowColor: 'rgba(105,123, 214, 0.7)'
                }
            },
            zlevel: 2,
            barWidth: '20%',
            data: data,
        },
        {
            name: '',
            type: 'bar',
            xAxisIndex: 1,
            zlevel: 1,
            itemStyle: {
                normal: {
                    color: 'transparent',
                    borderWidth: 0,
                    shadowBlur: {
                        shadowColor: 'rgba(255,255,255,0.31)',
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowOffsetY: 2,
                    },
                }
            },
            barWidth: '20%',
            data: [30, 30, 30, 30, 30]
        }
        ]
    }


    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    window.addEventListener("resize", function () {
        myChart.resize();
    });
}
echarts_5();
echarts_6();
