var CommonHelper = {
    // Get parameters from Url
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        }
        else {
            return undefined;
        }
    },
    //2
    //时间戳转换
    GetDataString: function (timestamp) {
        if (CommonHelper.IsNullOrWhiteSpace(timestamp)) {
            return "";
        }
        var timestampDate = timestamp.replace("/Date(", "").replace(")/", "");
        var tempDate = new Date(parseInt(timestampDate));
        Y = tempDate.getFullYear() + "-";
        M = (tempDate.getMonth() + 1 < 10 ? "0" + (tempDate.getMonth() + 1) : tempDate.getMonth() + 1) + "-";
        D = tempDate.getDate() < 10 ? "0" + tempDate.getDate() : tempDate.getDate();
        h = tempDate.getHours() < 10 ? "0" + tempDate.getHours() : tempDate.getHours();
        m = tempDate.getMinutes() < 10 ? "0" + tempDate.getMinutes() : tempDate.getMinutes();
        s = tempDate.getSeconds() < 10 ? "00" : tempDate.getSeconds();
        return (Y + M + D + " " + h + ":" + m + ":" + s);
    },
    //字符串是否有有效值 为空返回真
    IsNullOrWhiteSpace: function (stringValue) {
        var returnValue = false;
        if (typeof (stringValue) == "string") {
            stringValue = stringValue.replace(/(^\s*)|(\s*$)/g, "");
            if (stringValue == "") {
                return true;
            }
            return false;
        }
        else {
            return true;
        }
    },
    ParseInt: function (num, digits) {
        digits = Math.pow(10, digits);
        return Math.round(num * digits) / digits;
    }
};

var Zyt_Common = {
    avValueInvalid: 0,
    avValue: 1,
    avLbsValue: 2,
    avLbsValue_lbsNew: 1,
    //获取点 0  全为0
    GetPoint: function (item) {
        var notConvertPoint;
        var iconNum;
        if (item.GpsAv == Zyt_Common.avValue) {
            notConvertPoint = new BMap.Point(item.Lng, item.Lat);
            //不在线
            if (item.IsOnLine > 30) {
                iconNum = 3;
            }
                //在线
            else {
                if (item.IsOverStop > 30) {
                    //停车超时
                    iconNum = 4;
                }
                else {
                    if (item.Veo == 0) {
                        //停车
                        iconNum = 2;
                    }
                    else {
                        //运行
                        iconNum = 1;
                    }
                }
            }
        }
            //gps 不定位 lbs定位
        else {
            if (item.GpsAv == Zyt_Common.avValueInvalid && item.LbsAv == Zyt_Common.avLbsValue_lbsNew) {
                if (item.Lng == '0' || item.Lat == '0') {
                    if (item.Lng2 == '0' || item.Lat2 == '0') {
                        notConvertPoint = 0;
                        iconNum = 0;
                    }
                    else {
                        notConvertPoint = new BMap.Point(item.Lng2, item.Lat2);
                        //基站
                        iconNum = 51;
                    }
                }
                    //gps 不为0
                else {
                    //lbs 为0
                    if (item.Lng2 == '0' || item.Lat2 == '0') {
                        notConvertPoint = new BMap.Point(item.Lng, item.Lat);
                        //不在线
                        if (item.IsOnLine > 30) {
                            iconNum = 61;
                        }
                            //在线
                        else {
                            iconNum = 71;
                        }
                    }
                    else {
                        //超过10
                        if (item.GpsLbsDistance > 10) {
                            notConvertPoint = new BMap.Point(item.Lng2, item.Lat2);
                            iconNum = 5;

                        }
                        else {
                            notConvertPoint = new BMap.Point(item.Lng, item.Lat);
                            //不在线
                            if (item.IsOnLine > 30) {
                                iconNum = 6;
                            }
                                //在线
                            else {
                                iconNum = 7;
                            }
                        }
                    }
                }
            }
            else {
                if (item.Lng == '0' || item.Lat == '0') {
                    //找lbs
                    if (item.Lng2 == '0' || item.Lat2 == '0') {
                        notConvertPoint = 0;
                        iconNum = 0;
                    }
                    else {
                        notConvertPoint = new BMap.Point(item.Lng2, item.Lat2);
                        //基站
                        iconNum = 51;
                    }

                }
                else {
                    //lbs 为0
                    if(item.Lng2 == '0' || item.Lat2 == '0') {
                        notConvertPoint = new BMap.Point(item.Lng, item.Lat);
                        //不在线
                        if (item.IsOnLine > 30) {
                            iconNum = 61;
                        }
                            //在线
                        else {
                            iconNum = 71;
                        }
                    }
                }
                //notConvertPoint = 0;
                //iconNum = 0;
            }
        }
        item.bPoint2 = notConvertPoint;
        item.iconNum = iconNum;
        return item;
    },
    //拼接显示窗口的数据 显示btn
    LocLableText_GpsBtn: function (vBaseData, vReplayDataItem, vID) {
        if (vReplayDataItem == null || vReplayDataItem.GpsTime == null) {

        }
        else {
            var recdate = CommonHelper.GetDataString(vReplayDataItem.RecveTime);
            var gpsdate = CommonHelper.GetDataString(vReplayDataItem.GpsTime);
            var rec = new Date(vReplayDataItem.RecveTime.toDateTime());
            var gps = new Date(vReplayDataItem.GpsTime.toDateTime());
            var isred = parseInt((rec.getTime() - gps.getTime()) / 1000) > 30 * 60 ? "red" : "";
        }
        var info = (vReplayDataItem.GpsAv == 0) ? "车辆处于卫星信号盲区，无法定位。" : "加载中...";
        switch (vBaseData.iconNum) {
            case 6:
            case 7:
                return "<div class='kuang'>" +
                 "<div class='Mapline'></div>" +
                 "<div id=detail_info" + vID + " class='detail_info'>  " +
                     "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                     "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                     "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                     "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                     //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                     "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                     "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                     "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     //"<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                     "<p id=detail_info_s7><button onclick='ShowLastPosition()'>显示基站位置</button></p>" +//<span id='lastgps'></span>
                 "<p></p></div>" +
                 "</div>";
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 51:
            case 61:
            case 71:
                //停车
                return "<div class='kuang'>" +
                     "<div class='Mapline'></div>" +
                     "<div id=detail_info" + vID + " class='detail_info'>  " +
                         "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                         "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                         "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                         "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                         //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                         "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                         "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                         "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     "<p></p></div>" +
                     "</div>";
                break;
            case 5:
                //卫星
                return "<div class='kuang'>" +
                    "<div class='Mapline'></div>" +
                    "<div id=detail_info" + vID + " class='detail_info'>  " +
                        "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                        "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                        "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                        "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                        //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                        "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                        "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                        "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                        //"<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                        "<p id=detail_info_s7><button onclick='ShowLastPosition()'>显示卫星位置</button></p>" +//<span id='lastgps'></span>
                    "<p></p></div>" +
                    "</div>";
                break;
        }
    },
    LocLableText_GpsBtn_App: function (vBaseData, vReplayDataItem, vID) {
        if (vReplayDataItem == null || vReplayDataItem.GpsTime == null) {

        }
        else {
            var recdate = CommonHelper.GetDataString(vReplayDataItem.RecveTime);
            var gpsdate = CommonHelper.GetDataString(vReplayDataItem.GpsTime);
            var rec = new Date(vReplayDataItem.RecveTime.toDateTime());
            var gps = new Date(vReplayDataItem.GpsTime.toDateTime());
            var isred = parseInt((rec.getTime() - gps.getTime()) / 1000) > 30 * 60 ? "red" : "";
        }
        var info = (vReplayDataItem.GpsAv == 0) ? "车辆处于卫星信号盲区，无法定位。" : "加载中...";
        switch (vBaseData.iconNum) {
            case 6:
            case 7:
                return "<div class='kuang'>" +
                 "<div class='Mapline'></div>" +
                 "<div id=detail_info" + vID + " class='detail_info_app'>  " +
                     "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                     "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                     "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                     "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                     //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                     "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                     "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                     "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     //"<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                     "<p id=detail_info_s7><button onclick='ShowLastPosition()'>显示基站位置</button></p>" +//<span id='lastgps'></span>
                 "<p></p></div>" +
                 "</div>";
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 51:
            case 61:
            case 71:
                //停车
                return "<div class='kuang'>" +
                     "<div class='Mapline'></div>" +
                     "<div id=detail_info" + vID + " class='detail_info_app'>  " +
                         "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                         "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                         "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                         "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                         //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                         "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                         "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                         "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     "<p></p></div>" +
                     "</div>";
                break;
            case 5:
                //卫星
                return "<div class='kuang'>" +
                    "<div class='Mapline'></div>" +
                    "<div id=detail_info" + vID + " class='detail_info_app'>  " +
                        "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                        "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                        "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                        "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                        //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                        "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                        "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                        "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                        //"<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                        "<p id=detail_info_s7><button onclick='ShowLastPosition()'>显示卫星位置</button></p>" +//<span id='lastgps'></span>
                    "<p></p></div>" +
                    "</div>";
                break;
        }
    },
    //图片位置 0点击 1实时数据
    GetIcon_New: function (kinds, item) {
        var imgUrl = "../images/zytmapmarker.gif";
        var tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -93) });
        if (kinds == 0) {
            tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -93) });
        }
        else if (kinds == 1) {
            switch (item.iconNum) {
                case 1:
                    //运行
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -63) });
                    break;
                case 2:
                    //停车
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -33) });
                    break;
                case 3:
                    //带方向
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -3) });
                    break;
                case 4:
                    //停车超时
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(0, -213) });
                    break;
                case 5:
                case 51:
                    var tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-240, -183) });
                    break;
                case 6:
                case 61:
                    //灰色问号
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-30, -213) });
                    break;
                case 7:
                case 71:
                    //绿色问号
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-60, -213) });
                    break;
            }
        }
        return tempIcon;
    },
    //图片位置 0点击 1实时数据
    GetIcon: function (kinds, item) {
        var imgUrl = "../images/zytmapmarker.gif";
        var tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -93) });
        if (kinds == 0) {
            tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -93) });
        }
        else if (kinds == 1) {
            if (item.IsOnLine > 30) {
                if (item.GpsAv == Zyt_Common.avValue) {//gps
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -3) });
                }
                else if (item.GpsAv == Zyt_Common.avLbsValue) {
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-240, -3) });
                }
                else { tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-30, -213) }); }

            }
            else {
                if (item.GpsAv == Zyt_Common.avValue) {//gps
                    if (item.Veo == 0) {
                        tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -33) });
                    }
                    else {
                        tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(Zyt_Common.GetIconX(item.Direct), -63) });
                    }
                }
                else if (item.GpsAv == Zyt_Common.avLbsValue) {
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-240, -183) });
                }
                else if (item.GpsAv == 0) {
                    tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-60, -213) });
                }
            }


        }
        return tempIcon;
    },
    //图片位置偏移
    GetIconX: function (direction) {
        var ret = 0;
        if ((direction >= 0 && direction <= 23) || (direction > 338)) {
            ret = 0;
        }
        else if (direction > 23 && direction <= 68) {
            ret = -30;
        }
        else if (direction > 68 && direction <= 113) {
            ret = -60;
        }
        else if (direction > 113 && direction <= 158) {
            ret = -90;
        }
        else if (direction > 158 && direction <= 203) {
            ret = -120;
        }
        else if (direction > 203 && direction <= 248) {
            ret = -150;
        }
        else if (direction > 248 && direction <= 293) {
            ret = -180;
        }
        else if (direction > 293 && direction <= 338) {
            ret = -210;
        }
        return ret;
    },
    //图片位置偏移
    GetIconX_Play: function (direction) {
        var ret = 0;
        if ((direction >= 0 && direction <= 23) || (direction > 338)) {
            ret = 0;
        }
        else if (direction > 23 && direction <= 68) {
            ret = -48;
        }
        else if (direction > 68 && direction <= 113) {
            ret = -96;
        }
        else if (direction > 113 && direction <= 158) {
            ret = -144;
        }
        else if (direction > 158 && direction <= 203) {
            ret = -192;
        }
        else if (direction > 203 && direction <= 248) {
            ret = -240;
        }
        else if (direction > 248 && direction <= 293) {
            ret = -288;
        }
        else if (direction > 293 && direction <= 338) {
            ret = -336;
        }
        return ret;
    },
    //图片位置偏移
    GetIconX_PlayApp: function (direction) {
        var ret = 0;
        if ((direction >= 0 && direction <= 23) || (direction > 338)) {
            ret = 0;
        }
        else if (direction > 23 && direction <= 68) {
            ret = -38;
        }
        else if (direction > 68 && direction <= 113) {
            ret = -72;
        }
        else if (direction > 113 && direction <= 158) {
            ret = -110;
        }
        else if (direction > 158 && direction <= 203) {
            ret = -148;
        }
        else if (direction > 203 && direction <= 248) {
            ret = -186;
        }
        else if (direction > 248 && direction <= 293) {
            ret = -224;
        }
        else if (direction > 293 && direction <= 338) {
            ret = -262;
        }
        return ret;
    },
    //图片位置 
    GetLastLbsIcon: function () {
        var imgUrl = "../images/zytmapmarker.gif";
        var tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-240, -183) });
        return tempIcon;
    },
    //图片位置 0点击 1实时数据
    GetLastGpsIcon: function () {
        var imgUrl = "../images/zytmapmarker.gif";
        var tempIcon = new BMap.Icon(imgUrl, new BMap.Size(24, 24), { offset: new BMap.Size(0, 0), imageOffset: new BMap.Size(-269, -183) });
        return tempIcon;
    },
    //方向描述
    DirectionText: function (direction) {
        var ret = "正北";
        if ((direction >= 0 && direction <= 23) || (direction > 338)) {
            ret = "正北";
        }
        else if (direction > 23 && direction <= 68) {
            ret = "东北";
        }
        else if (direction > 68 && direction <= 113) {
            ret = "正东";
        }
        else if (direction > 113 && direction <= 158) {
            ret = "东南";
        }
        else if (direction > 158 && direction <= 203) {
            ret = "正南";
        }
        else if (direction > 203 && direction <= 248) {
            ret = "西南";
        }
        else if (direction > 248 && direction <= 293) {
            ret = "正西";
        }
        else if (direction > 293 && direction <= 338) {
            ret = "西北";
        }
        return ret;
    },
    //拼接显示窗口的数据
    LocLableText: function (vBaseData, vReplayDataItem, vID) {
        if (vReplayDataItem == null || vReplayDataItem.GpsTime == null) {

        }
        else {
            var recdate = CommonHelper.GetDataString(vReplayDataItem.RecveTime);
            var gpsdate = CommonHelper.GetDataString(vReplayDataItem.GpsTime);
            var rec = new Date(vReplayDataItem.RecveTime.toDateTime());
            var gps = new Date(vReplayDataItem.GpsTime.toDateTime());
            var isred = parseInt((rec.getTime() - gps.getTime()) / 1000) > 30 * 60 ? "red" : "";
        }

        var info = (vReplayDataItem.GpsAv == 0) ? "车辆处于卫星信号盲区，无法定位。" : "加载中...";
        if (vReplayDataItem.GpsAv == Zyt_Common.avLbsValue) {
            return "<div class='kuang'>" +
                    "<div class='Mapline'></div>" +
                    "<div id=detail_info" + vID + " class='detail_info'>  " +
                        "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                        "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                        "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                        "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                        //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                        "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                        "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                        //"<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                        "<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                        //"<p id=detail_info_s7><button onclick='LastGps()'>最后卫星位置</button>：<span id='lastgps'></span></p>" +
                    "<p></p></div>" +
                    "</div>";
        }
        else {
            return "<div class='kuang'>" +
                     "<div class='Mapline'></div>" +
                     "<div id=detail_info" + vID + " class='detail_info'>  " +
                         "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                         "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                         "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                         "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                         //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                         "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                         "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                         "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     "<p></p></div>" +
                     "</div>";
        }
    },
    //拼接显示窗口的数据 显示btn
    OblsulteLocLableText_GpsBtn: function (vBaseData, vReplayDataItem, vID) {
        if (vReplayDataItem == null || vReplayDataItem.GpsTime == null) {

        }
        else {
            var recdate = CommonHelper.GetDataString(vReplayDataItem.RecveTime);
            var gpsdate = CommonHelper.GetDataString(vReplayDataItem.GpsTime);
            var rec = new Date(vReplayDataItem.RecveTime.toDateTime());
            var gps = new Date(vReplayDataItem.GpsTime.toDateTime());
            var isred = parseInt((rec.getTime() - gps.getTime()) / 1000) > 30 * 60 ? "red" : "";
        }

        var info = (vReplayDataItem.GpsAv == 0) ? "车辆处于卫星信号盲区，无法定位。" : "加载中...";
        if (vReplayDataItem.LbsAv == Zyt_Common.avLbsValue_lbsNew) {
            return "<div class='kuang'>" +
                    "<div class='Mapline'></div>" +
                    "<div id=detail_info" + vID + " class='detail_info'>  " +
                        "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                        "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                        "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                        "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                        //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                        "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                        "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                        "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                        //"<p id=detail_info_s7>基站位置：<span style='color:#14568A'>" + info + "</span></p>" +
                        "<p id=detail_info_s7><button onclick='LastLbs()'>显示基站定位</button></p>" +//<span id='lastgps'></span>
                    "<p></p></div>" +
                    "</div>";
        }
        else {
            return "<div class='kuang'>" +
                     "<div class='Mapline'></div>" +
                     "<div id=detail_info" + vID + " class='detail_info'>  " +
                         "<p id=detail_info_s1>时间：<span>" + recdate + "</span>&nbsp;&nbsp;&nbsp;&nbsp;GPS时间：<span style=\"color:" + isred + ";\">" + gpsdate + "</span></p>  " +
                         "<p id=detail_info_s4>定位方式：<span>" + Zyt_Common.LocationText(vReplayDataItem.GpsAv) + "</span></p>" +
                         "<p id=detail_info_s2>组名：<span>" + vBaseData.GroupInfo + " </span></p>" +
                         "<p id=detail_info_s3>速度：<span>" + vReplayDataItem.Veo + " km/h " + Zyt_Common.DirectionText(vReplayDataItem.Direct) + "</span></p>" +
                         //"<p id=detail_info_s4>方向：<span>" + vBaseData.Plate + "</span></p>" +
                         "<p id=detail_info_s5>状态：<span>" + vReplayDataItem.State + "</span></p>" +
                         "<p id=detail_info_s6>备注：<span>" + vBaseData.Remark + "</span></p>" +
                         "<p id=detail_info_s7>位置：<span id='infopos'>" + info + "</span></p>" +
                     "<p></p></div>" +
                     "</div>";
        }
    },
    //拼接显示窗口的数据 只包含位置信息
    LocLableText_PosInfo: function (vBaseData, vReplayDataItem, vID) {
        //return "<div class='kuang'>" +
        //         "<div class='Mapline'></div>" +
        //         "<div id=detail_info" + vID + " class='detail_info'>  " +
        //             "<p id=detail_info_s7 style=''>位置：<span id='infopos'>" + "加载中..." + "</span></p>" +
        //         "<p></p></div>" +
        //         "</div>";
        return "<div >" +
                     "<p id=detail_info_s7 style='font-family: 'Microsoft YaHei';font-size:14px;'>位置：<span id='infopos'>" + "加载中..." + "</span></p>" +
                 "</div>";
    },
    //定位方式
    LocationText: function (GpsAv) {
        var ret = "未知转态";
        switch (GpsAv) {
            case "0": ret = "不定位";
                break;
            case "1": ret = "Gps定位";
                break;
            case "2": ret = "基站定位";
                break;
        }
        return ret;
    },
    //标题
    LocLableTitle: function (plate) {
        return "<div class='Mapheader'><p>" + plate + "</p></div>";
    },
    //获取位置
    GetPointPostion: function (point) {
        //var convertor = new BMap.Convertor();
        var info = "无";
        //convertor.translate([point], 0, 5, function (pts) {
        if (point !== null) {
            var geoc = new BMap.Geocoder();
            geoc.getLocation(point, function (rs) {
                var addComp = rs.addressComponents;
                info = addComp.province + " " + addComp.city + " " + addComp.district + " " + addComp.street + " " + addComp.streetNumber;
                $("#detail_info_s7 span").html(info);
            });
        }
        // });
    },
    //获取百度位置
    GetPointPostionBD: function (point) {
        var convertor = new BMap.Convertor();
        var info = "无";
        convertor.translate([point], 0, 5, function (pts) {
            if (pts.point[0] !== null) {
                var geoc = new BMap.Geocoder();
                geoc.getLocation(pts.point[0], function (rs) {
                    var addComp = rs.addressComponents;
                    info = addComp.province + " " + addComp.city + " " + addComp.district + " " + addComp.street + " " + addComp.streetNumber;
                    $("#detail_info_s7 span").html(info);
                });
            }
        });
    },
    //经纬度不准确 不能判断
    ComparePoint: function (pointA, pointB) {
        if (typeof (pointA.lng) == "number" && typeof (pointA.lat) == "number" && typeof (pointB.lng) == "number" && typeof (pointB.lat) == "number") {
            if ((pointA.lng.toFixed(5) == pointB.lng.toFixed(5)) && (pointA.lat.toFixed(5) == pointB.lat.toFixed(5))) {
                return true;
            }
        }
        return false;
    },
    //圆样式
    CircleStyle: function (color) {
        return {
            strokeColor: color,    //边线颜色。"blue",//
            //fillColor: color,      //填充颜色。当参数为空时，圆形将没有填充效果。
            strokeWeight: 3,       //边线的宽度，以像素为单位。
            strokeOpacity: 0.8,	   //边线透明度，取值范围0 - 1。
            fillOpacity: 0.2,      //填充的透明度，取值范围0 - 1。
            strokeStyle: "dashed" //边线的样式，solid或dashed。
        };
    },
    //画圆
    DrawingCircle: function (point, radius, color) {
        var circle = new BMap.Circle(point, radius, Zyt_Common.CircleStyle(color));
        return circle;
    },
    //加载地图
    LoadMap(divid, mapObject) {
        mapObject = new BMap.Map(divid);
        //GPS坐标
        var x = 104.0622320000;
        var y = 30.5926630000;
        var ggPoint = new BMap.Point(x, y);

        //地图初始化
        mapObject.centerAndZoom(ggPoint, 19);
        mapObject.addControl(new BMap.NavigationControl());
        mapObject.enableScrollWheelZoom(true);
        mapObject.setZoom(15);
        //地图类型
        mapObject.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_SATELLITE_MAP] }));
        //比例尺
        var top_left_control = new BMap.ScaleControl();
        mapObject.addControl(top_left_control);
    },
    CreatMarker() {
    }
};