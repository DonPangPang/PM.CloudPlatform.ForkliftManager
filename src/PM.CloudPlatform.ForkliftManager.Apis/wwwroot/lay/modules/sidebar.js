/** kitadmin-v2.1.0 MIT License By http://kit.zhengjinfan.cn Author Van Zheng */ ;
"use strict";
var _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(i) {
	return typeof i
} : function(i) {
	return i && "function" == typeof Symbol && i.constructor === Symbol && i !== Symbol.prototype ? "symbol" : typeof i
};

function _defineProperty(i, e, t) {
	return e in i ? Object.defineProperty(i, e, {
		value: t,
		enumerable: !0,
		configurable: !0,
		writable: !0
	}) : i[e] = t, i
}
layui.define(["layer", "laytpl", "utils", "lodash"], function(i) {
	var e = layui.jquery,
		t = (layui.layer, layui.laytpl),
		n = layui.lodash,
		a = layui.utils,
		d = e("body"),
		o = function() {
			this.version = "1.0.0"
		},
		r = ['<div class="kit-sidebar" style="{{d.direction}}:-{{d.width}};width:{{d.width}};" kit-sidebar="{{d.id}}">',
			'<div class="kit-sidebar-body">', '  <div class="layui-card">', '    <div class="layui-card-header">',
			'      <span class="nowrap" title="{{d.title}}">{{d.title}}</span>',
			'      <div class="kit-sidebar-reload" title="刷新">', '        <i class="layui-icon">&#xe669;</i>', "      </div>",
			'      <div class="kit-sidebar-close" title="关闭">', '        <i class="layui-icon">&#x1006;</i>', "      </div>",
			"    </div>", '    <div class="layui-card-body">', "      {{d.content}}", "    </div>", "  </div>", "</div>",
			"</div>"
		].join(""),
		l = ['<div class="kit-sidebar-loading layui-anim layui-anim-fadein">', "    <div>",
			'        <i class="layui-icon layui-anim layui-anim-rotate layui-anim-loop">&#xe63e;</i>', "    </div>", "</div>"
		];
	o.prototype.defaults = {
		elem: void 0,
		content: "",
		shade: !1,
		shadeClose: !0,
		title: "未命名",
		direction: "right",
		dynamicRender: !1,
		url: void 0,
		width: "280px",
		done: void 0
	}, o.prototype.render = function(i) {
		var t = this,
			d = n.cloneDeep(t.defaults);
		e.extend(!0, d, i);
		var o = d;
		if (!a.oneOf(o.direction, ["left", "right"])) return a.error(
			'Sidebar error: [direction] property error,Only "left" or "right" .'), t;
		var r = {
			title: o.title,
			id: a.randomCode(),
			content: o.content,
			direction: o.direction,
			width: o.width
		};
		if (o.dynamicRender) {
			var l = o.url + "?version=" + (new Date).getTime();
			a.tplLoader(l, function(i) {
				r.content = i, c.renderHTML(o, r)
			}, function(i) {
				r.content = i, c.renderHTML(o, r)
			})
		} else c.renderHTML(o, r);
		return t
	};
	var c = {
		renderHTML: function(i, n) {
			var o = e(i.elem);
			void 0 === o.attr("kit-sidebar-target") && t(r).render(n, function(t) {
				i.shade && (t = t + '<div class="kit-shade" kit-shade="' + n.id + '"></div>'), d.append(t), "function" ==
					typeof i.done && i.done();
				var r = e('div[kit-sidebar="' + n.id + '"]'),
					l = e('div[kit-shade="' + n.id + '"]');
				o.attr("data-toggle", "off"), o.attr("kit-sidebar-target", "true"), o.on("click", function() {
					switch (e(this).data("toggle")) {
						case "on":
							r.animate(_defineProperty({}, i.direction, "-" + i.width)), l.hide(), e(this).data("toggle", "off");
							break;
						case "off":
							r.animate(_defineProperty({}, i.direction, "0px")), l.show(), e(this).data("toggle", "on")
					}
				}), "object" === _typeof(i.elem) && o.click(), i.shadeClose && l.on("click", function() {
					o.click()
				}), r.find(".kit-sidebar-reload").on("click", function() {
					var t = this;
					if (i.dynamicRender) {
						c.showLoading(r);
						var n = i.url + "?version=" + (new Date).getTime();
						a.tplLoader(n, function(i) {
							e(t).parent().next(".layui-card-body").html(i), c.hideLoading(r)
						}, function(i) {
							e(t).parent().next(".layui-card-body").html("Loading error:" + i), c.hideLoading(r)
						})
					}
				}), r.find(".kit-sidebar-close").on("click", function() {
					o.click()
				})
			})
		},
		showLoading: function(i) {
			i.append(l.join(""))
		},
		hideLoading: function(i) {
			setTimeout(function() {
				var e = i.find(".kit-sidebar-loading");
				e.addClass("layui-anim-fadeout"), setTimeout(function() {
					e.remove()
				}, 300)
			}, 500)
		}
	};
	i("sidebar", new o)
});
//# sourceMappingURL=sidebar.js.map
