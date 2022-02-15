/**
 * date:2021/03/09
 * author:Wang
 * description:此处存放封装的插件,此文件需配合untils.css一起使用
 */

/**
 * 页面内导航面板
 * @requires jquery
 * @param {arr：Array} { title：标题，value：导航项的值，isShow：默认显示的面板}
 * @param {callback} 导航栏点击的项，返回点击对象所绑定的value
 * @description  此方法只生成导航栏，点击返回value，_this样式并不涉及，需在页面内自行设计
 */
 $.fn.pmPanel = function (arr,callback) {
	let _this = $(this);
    _this.addClass('pm-panel');
    // 循环向_this创建导航项dom
    for(let v of arr){
        v.isShow == true ?
            _this.append(`<div class="pm-nav-item pm-nav-item-active" data-value=${ v.value }>${ v.title }</div>`) :
                 _this.append(`<div class="pm-nav-item" data-value=${ v.value }>${ v.title }</div>`);
    }
    // 导航项hover事件
    $(".pm-nav-item").hover(
        function () {
            $(this).hasClass('pm-nav-item-active') ?
                $(this).addClass('pm-nav-item-active-hover') :
                    $(this).addClass('pm-nav-item-hover');
            $(this).addClass("pm-nav-item-hover");
        },
        function () {
            $(this).hasClass('pm-nav-item-active') ?
                $(this).removeClass('pm-nav-item-active-hover') :
                    $(this).removeClass('pm-nav-item-hover');
            $(this).removeClass("pm-nav-item-hover");
        }
    );
    // 导航项点击事件
    _this.on("click",".pm-nav-item",function(){
        if(!$(this).hasClass('pm-nav-item-active')){
            $(this).addClass('pm-nav-item-active').siblings().removeClass('pm-nav-item-active');
            callback($(this).attr('data-value'));
        }
    })
};