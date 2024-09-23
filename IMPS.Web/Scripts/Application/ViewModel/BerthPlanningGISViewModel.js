(function (IPMSRoot) {
    var isView = 0;
    var map;
    var polyline1 = null;
    var marker1 = null;
    var markers1;

    var BerthPlanningGISViewModel = function () {
        var self = this;

        var infowindow = [];
        var marker = [];
        var tooltip;
        var vessel1;
        var vessel2;

        self.markers = ko.observableArray();

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.berthplanninggisModel = ko.observable(new IPMSRoot.BerthPlanningGISModel());
        self.StartDate = ko.observable();
        self.EndDate = ko.observable();
        self.VesselsList = ko.observableArray();

        self.quayberthsWithBollards = ko.observableArray();
        self.PlannedVessels = ko.observableArray();
        self.AnchoredVessels = ko.observableArray();//Anchoredcode
        self.BerthedVessels = ko.observableArray();
        self.gisMapPath = ko.observable();
        //    self.PendingVessels = ko.observableArray();
        var portcodegis = "";
        self.Initialize = function () {

            var decryptresult = "";
          
            var portgischeck = $.cookie('Portcodecheck');
            debugger;

            // if (portgischeck == "true") 
            if (portgischeck == "true" || (window.location.href).split('/')[(window.location.href).split('/').length - 1] == "DeskBerthPlanningGIS")
            {
                portgischeck = "";
                portcodegis = $.cookie('PortcodeGIS');
                $.cookie('Portcodecheck', 'false', { path: '/' });
               
            }
            else 
            {
                portcodegis = $.cookie('Port');

            }
           
            for (var i = 0; i < portcodegis.length; i++) {

                decryptresult += String.fromCharCode(portcodegis.charCodeAt(i) ^ 6);
            }
            portcodegis = decryptresult;


            self.berthplanninggisModel(new IPMSROOT.BerthPlanningGISModel());
            self.LoadVesselCallMovements();

            self.LoadAnchorageVesselCall();//anchored vessel code
            self.LoadMap();
            self.PlotBerthedVessels();
            self.PlotAnchoredVessels();//anchored vessel code
            //self.viewModelHelper();
            tooltip = new Opentip(
              "div#GridContainer", //target element 
              "DummyContent", // will be replaced
              "", // title
              {
                  showOn: null,
                  // I'll manually manage the showOn effect
              });
        }

        self.VesselDetailsPop = function (data, event) {

            var vesselinfo = getVesselInfo(data);
            tooltip.setContent(vesselinfo);
            tooltip.show();


        }
        
        function getVesselInfo(data) {
            var data;

            if (data.hasOwnProperty("MovementStatus")) {
            if ((data.MovementType() == "ARMV" || data.MovementType() == "SHMV" || data.MovementType() == "WRMV") && data.MovementStatus() == "BERT" || data.MovementStatus() == "SALD") {
                data =
                    "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                    "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardCode() + " to " + data.ToBollardCode() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.LOA() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Draft:</td><td>" + data.Draft() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Mooring Bollards:</td><td>" + data.MooringBowBollard() + " to " + data.MooringStemBollard() + "</td></tr>";

            }
            else {
                data =
                    "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                    "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardCode() + " to " + data.ToBollardCode() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.LOA() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Draft:</td><td>" + data.Draft() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                    "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>";

            }
            }
            else {

                data = "<table class='tooltipcss' id='" + data.VCN() + "'>" +
                      "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>DWT(t):</td><td>" + data.DeadWeightTonnageInMT() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>IMO:</td><td>" + data.IMONo() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                      "<tr><td class='tooltipheader'>Next Port:</td><td>" + data.NextPortOfCall() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>ETA:</td><td>" + data.ETA() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>ETD:</td><td>" + data.ETD() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Reasonforvisit:</td><td>" + data.ReasonforvisitName() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Cargo Type:</td><td>" + data.CargoTypes() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Arr.Draft(m):</td><td>" + data.ArrDraft() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Dep.Draft(m):</td><td>" + data.DepDraft() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Previous Port:</td><td>" + data.LastPortOfCall() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Anchorage Reason:</td><td>" + data.Reason() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Anchor Position:</td><td>" + data.AnchorPosition() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>DraftBearing & Distance:</td><td>" + data.BearingDistanceFromBreakWater() + "</td></tr>" +
                      "<tr><td class='tooltipheader'> Port of Registry:</td><td>" + data.PortOfRegistry() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>LOA(m):</td><td>" + data.LengthOverallInM() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Beam(m):</td><td>" + data.BeamInM() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>GRT(m):</td><td>" + data.GrossRegisteredTonnageInMT() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Flag :</td><td>" + data.VesselNationality() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Agent :</td><td>" + data.AgentName() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Built:</td><td>" + data.VesselBuildYear() + "</td></tr>";
            }
            return data;
        }
        self.LoadMap = function () {

                self.viewModelHelper.apiGet('api/GetGisMapPath/' + portcodegis, null,
                   function (result) {
                       self.gisMapPath(result);
                   }, null, null, false);     

            var gisLocation = self.gisMapPath().geographicLocation.split(',');
            google.maps.visualRefresh = true;
            //var Durban = new google.maps.LatLng(-29.873244000000000000, 31.024533000000020000);
            var Durban = new google.maps.LatLng(gisLocation[0], gisLocation[1]);
            //marker images for anchored
            var anchoredControlDiv = document.createElement('div');
            var controlanchoredUI = document.createElement('div');
            controlanchoredUI.style.cursor = 'pointer';
            controlanchoredUI.title = 'Click to see the Anchored Vessels';
            controlanchoredUI.className = 'map-anchored';
            anchoredControlDiv.appendChild(controlanchoredUI);
            // Setup the click event listeners: simply set the map to anchored.
            controlanchoredUI.addEventListener('click', function () {
                var htmldataBody = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: '/api/GetAnchoredVesselDetails/' + portcodegis,
                    data: "",
                    dataType: "json",
                    success: function (data) {

                        ko.utils.arrayForEach(data, function (val) {
                            htmldataBody += '<tr>';
                            htmldataBody += '<td>' + val.VesselName + '</td>';
                            htmldataBody += '<td>' + val.VCN + '</td>';
                            htmldataBody += '<td>' + val.AnchorageReason + '</td>';
                            htmldataBody += '<td>' + val.AnchorPosition + '</td>';
                            htmldataBody += '<td>' + val.BearingDistanceFromBreakWater + '</td>';
                            htmldataBody += '<td>' + val.AgentName + '</td>';
                            htmldataBody += '</tr>';
                        });
                        $("#tblanchored").find("tr:not(:first)").remove();
                        $('#tblanchored').append(htmldataBody);
                        $('#popupdivanchored').modal('show');

                    }
                });
                return false;
            });
            //berthed
            var berthedControlDiv = document.createElement('div');
            var controlberthedUI = document.createElement('div');
            controlberthedUI.style.cursor = 'pointer';
            controlberthedUI.title = 'Click to see the Berthed Vessels';
            controlberthedUI.className = 'map-berthed';
            berthedControlDiv.appendChild(controlberthedUI);
            // Setup the click event listeners: simply set the map to Berthed.
            controlberthedUI.addEventListener('click', function () {
                var htmldataBody = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",

                    url: '/api/GetBerthedVesselDetails/' + portcodegis,
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        ko.utils.arrayForEach(data, function (val) {
                            htmldataBody += '<tr>';
                            htmldataBody += '<td>' + val.VesselName + '</td>';
                            htmldataBody += '<td>' + val.VCN + '</td>';
                            htmldataBody += '<td>' + val.ReasonforVisitName + '</td>';
                            htmldataBody += '<td>' + val.ATA + '</td>';
                            htmldataBody += '<td>' + val.ATB + '</td>';
                            htmldataBody += '<td>' + val.BerthName + '</td>';
                            htmldataBody += '<td>' + val.AgentName + '</td>';
                            htmldataBody += '</tr>';
                        });
                        $("#tblberthed").find("tr:not(:first)").remove();
                        $('#tblberthed').append(htmldataBody);
                        $('#popupdivberthed').modal('show');
                    }
                });
            });

            //sailed
            var sailedControlDiv = document.createElement('div');
            var controlsailedUI = document.createElement('div');
            controlsailedUI.style.cursor = 'pointer';
            controlsailedUI.title = 'Click to see the Sailed Vessels';
            controlsailedUI.className = 'map-sailed';
            sailedControlDiv.appendChild(controlsailedUI);
            // Setup the click event listeners: simply set the map to Sailed.
            controlsailedUI.addEventListener('click', function () {
                var htmldataBody = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: '/api/GetSailedVesselDetails/' + portcodegis,
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        ko.utils.arrayForEach(data, function (val) {
                            htmldataBody += '<tr>';
                            htmldataBody += '<td>' + val.VesselName + '</td>';
                            htmldataBody += '<td>' + val.VCN + '</td>';
                            htmldataBody += '<td>' + val.ATUB + '</td>';
                            htmldataBody += '<td>' + val.ATD + '</td>';
                            htmldataBody += '<td>' + val.PortName + '</td>';
                            htmldataBody += '<td>' + val.AgentName + '</td>';
                            htmldataBody += '</tr>';
                        });
                        $("#tblsailed").find("tr:not(:first)").remove();
                        $('#tblsailed').append(htmldataBody);
                        $('#popupdivsailed').modal('show');
                    }
                });
            });
            //These are options that set initial zoom level, where the map is centered globally to start, and the type of map to show
             var zoomlevel = "";
             if (portcodegis == "MB")
                zoomlevel = 16;
            else
                zoomlevel = 15;
            var mapOptions = {
                    zoom: zoomlevel,
                center: Durban,
                mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
            };

            // This makes the div with id "map_canvas" a google map
            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
            map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(controlanchoredUI);
            map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(controlberthedUI);
            map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(controlsailedUI);
            //var centerControlDiv = document.createElement('div');
            //var centerControl = new CenterControl(centerControlDiv, map);

            //centerControlDiv.index = 1;
            //map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(centerControlDiv);


            //*Durban KMZ Layers*//
            var quayLayer = new google.maps.KmlLayer({

                //url: 'http://115.115.191.61:2226/images/KMZ/DBNquays.KMZ'
                url: self.gisMapPath().mapPath + 'Quays.KMZ'
            });

            var berthLayer = new google.maps.KmlLayer({

                url: self.gisMapPath().mapPath + 'Berths.KMZ'
            });

            var bollardLayer = new google.maps.KmlLayer({

                url: self.gisMapPath().mapPath + 'Bollards.KMZ'
            });

            quayLayer.setMap(map);
            berthLayer.setMap(map);
            bollardLayer.setMap(map);

        }

        self.LoadVesselCallMovements = function () {
            self.StartDate(new Date());
            var todate = new Date(self.StartDate());
            self.EndDate(moment(todate).add('days', 1).format('MM-DD-YYYY'));
            self.viewModelHelper.apiGet('api/GetVesselCallMovementsGIS/' + portcodegis +'/'+ kendo.toString(self.StartDate(), "yyyy-MM-dd") + '/' + kendo.toString(self.EndDate(), "yyyy-MM-dd"), null,
                 function (result) {
                     $.each(result, function (index, item) {
                         
                         self.PlannedVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
                         self.VesselsList.push(new IPMSRoot.BerthPlanningGISModel(item));
                         //if (item.MovementStatus == "MPEN" || item.MovementStatus == null) {
                         //       self.PendingVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
                         //}
                         //else {
                         //console.log('VCN' ,  item.VCN +'/'+item.MovementStatus);
                         //if (item.MovementStatus != "MPEN" && item.MovementStatus != "SALD")
                         //{
                         //    self.PlannedVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
                         //    self.VesselsList.push(new IPMSRoot.BerthPlanningGISModel(item));
                         //}
                         //}
                     });
                 },
                null, null, false);
        }

        // code foe anchoraged vessels

        self.LoadAnchorageVesselCall = function () {
            //  var portcode1 = $.cookie('Port');
            //  var decryptresult1 = '';
            //  for (var i = 0; i < portcode1.length; i++) {
            //      decryptresult1 += String.fromCharCode(portcode1.charCodeAt(i) ^ 6);
            //  }
            //  portcode1 = decryptresult1;
            self.viewModelHelper.apiGet('api/GetAnchorVesselInfoGIS/' + portcodegis, null,
                 function (result) {
                     $.each(result, function (index, item) {

                         self.AnchoredVessels.push(new IPMSRoot.AnchoredVesselGISModel(item));

                         //self.VesselsList.push(new IPMSRoot.AnchoredVesselGISModel(item));
                     });
                 },
                null, null, false);
        }
        self.PlotBerthedVessels = function () {
            $.each(self.PlannedVessels(), function (index, plannedvessel) {
                if (plannedvessel.MovementStatus() == "BERT") {
                    //console.log('berthedvessel', plannedvessel);
                    self.PlotVessel(plannedvessel);
                }

            });

        }
        //Anchred vessel code
        // var countvar = 0;
        self.PlotAnchoredVessels = function () {

            //countvar = 0;
            $.each(self.AnchoredVessels(), function (index, Anchoredvessel) {
                self.PlotVessel(Anchoredvessel);
            });
        }
        //self.plotAnchoredVessels = function () {
        //    $.each(self.PlannedVessels(), function (index) {

        //    });
        //}
        self.PlotVessel = function (vessel) {

            if (vessel.hasOwnProperty("MovementStatus")) {
                if (vessel.FromCoordinates() != null && vessel.ToCoordinates() != null && vessel.FromOffsetCoordinates() != null && vessel.ToOffsetCoordinates() != null && vessel.FromMidCoordinates() != null && vessel.ToMidCoordinates() != null && vessel.precoord() != null && vessel.preoffsetcord() != null) {
                var Coordinates = vessel.FromCoordinates().split(',');
                var toolTip = getVesselInfo(vessel);
                var image = '../Content/Images/vesselbert.png';

                var myLatLng = new google.maps.LatLng(parseFloat(Coordinates[0]), parseFloat(Coordinates[1]));

                var FromCoordinates = vessel.FromCoordinates().split(',');
                var ToCoordinates = vessel.ToCoordinates().split(',');
             ///   alert(vessel.FromOffsetCoordinates());
                var FromOffsetCoordinates = vessel.FromOffsetCoordinates().split(',');
                var ToOffsetCoordinates = vessel.ToOffsetCoordinates().split(',');
                var FromMidCoordinates = vessel.FromMidCoordinates().split(',');
                var ToMidCoordinates = vessel.ToMidCoordinates().split(',');
                var preCoordinates = vessel.precoord().split(',');
                var preOffsetCoordinates = vessel.preoffsetcord().split(',');
                var bollardCoordinates = [
                new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
                new google.maps.LatLng(parseFloat(FromOffsetCoordinates[0]), parseFloat(FromOffsetCoordinates[1])),
                new google.maps.LatLng(parseFloat(preOffsetCoordinates[0]), parseFloat(preOffsetCoordinates[1])),
                new google.maps.LatLng(parseFloat(ToMidCoordinates[0]), parseFloat(ToMidCoordinates[1])),
                new google.maps.LatLng(parseFloat(preCoordinates[0]), parseFloat(preCoordinates[1])),
                new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
                //new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
                //new google.maps.LatLng(parseFloat(FromOffsetCoordinates[0]), parseFloat(FromOffsetCoordinates[1])),
                //new google.maps.LatLng(parseFloat(ToOffsetCoordinates[0]), parseFloat(ToOffsetCoordinates[1])),
                //new google.maps.LatLng(parseFloat(ToCoordinates[0]), parseFloat(ToCoordinates[1])),
                //new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
                ];

                var vessel1 = new google.maps.Polygon({
                    paths: bollardCoordinates,
                    strokeColor: '#FF69B4',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#FF69B4',
                    fillOpacity: 0.5
                });

                //  var infowindow = new google.maps.InfoWindow({
                //    content: toolTip,
                //    suppressMapPan:true
                //});
                //eventPolygonClick = google.maps.event.addListener(vessel1, 'click', function (event) {
                //    infowindow.setPosition(event.latLng);
                //    infowindow.open(map, vessel1);
                //});

                //vessel1.pId = "state poly pId";
                vessel1.wPet = toolTip;
                vessel1.infoWindow = new google.maps.InfoWindow();

                google.maps.event.addListener(vessel1, "mouseover", function (event) {

                    this.infoWindow.setContent(this.wPet);
                    this.infoWindow.open(map);
                    this.infoWindow.setPosition(event.latLng);

                });

                google.maps.event.addListener(vessel1, "mouseout", function () {
                    this.infoWindow.close();
                });

                vessel1.setMap(map);
            }
            }

            else {

                // if (countvar<1)
                if (vessel.Coordinates() != null) {
                    //countvar++;

                    var Coordinates = vessel.Coordinates().split(',');
                    var toolTip = getVesselInfo(vessel);
                    var image = '../Content/Images/arrows16X16.png';

                    //var myLatLng = new google.maps.LatLng(parseFloat(Coordinates[0]), parseFloat(Coordinates[1]));

                    //new
                    var marker = new google.maps.Marker({
                        position: myLatLng,
                        map: map,
                        icon: image
                    });
                    marker.wPet = toolTip;
                    marker.infoWindow = new google.maps.InfoWindow();
                    google.maps.event.addListener(marker, "click", function (event) {

                        this.infoWindow.setContent(this.wPet);
                        // this.infoWindow.setPosition(event.latLng);
                        this.infoWindow.open(map, marker);

                    });

                    google.maps.event.addListener(marker, "mouseout", function () {
                        // this.infoWindow.close();
                    });

                    marker.setMap(map);

                }
            }
        }

        //Anchored vessel code
        self.MouseOut = function (data, event) {
            tooltip.hide();
            //anchored code
        }
        self.VesselClick = function (e) {
            var vessel = e;
            if (vessel.FromCoordinates() != null && vessel.ToCoordinates() != null && vessel.precoord() != null && vessel.preoffsetcord() != null) {
                var Coordinates1 = vessel.FromCoordinates().split(',');
                var toolTip1 = getVesselInfo(vessel);
                var myLatLng = new google.maps.LatLng(parseFloat(Coordinates1[0]), parseFloat(Coordinates1[1]));
                var bollardCoordinates1 = [];
                var FromCoordinates1 = vessel.FromCoordinates().split(',');
                var ToCoordinates1 = vessel.ToCoordinates().split(',');
                var FromOffsetCoordinates1 = vessel.FromOffsetCoordinates().split(',');
                var ToOffsetCoordinates1 = vessel.ToOffsetCoordinates().split(',');
                var FromMidCoordinates1 = vessel.FromMidCoordinates().split(',');
                var ToMidCoordinates1 = vessel.ToMidCoordinates().split(',');
                var preCoordinates1 = vessel.precoord().split(',');
                var preOffsetCoordinates1 = vessel.preoffsetcord().split(',');
                var bollardCoordinates1 = [
                new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
                new google.maps.LatLng(parseFloat(FromOffsetCoordinates1[0]), parseFloat(FromOffsetCoordinates1[1])),
                new google.maps.LatLng(parseFloat(preOffsetCoordinates1[0]), parseFloat(preOffsetCoordinates1[1])),
                new google.maps.LatLng(parseFloat(ToMidCoordinates1[0]), parseFloat(ToMidCoordinates1[1])),
                new google.maps.LatLng(parseFloat(preCoordinates1[0]), parseFloat(preCoordinates1[1])),
                new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
                //new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
                //new google.maps.LatLng(parseFloat(FromOffsetCoordinates1[0]), parseFloat(FromOffsetCoordinates1[1])),
                //new google.maps.LatLng(parseFloat(ToOffsetCoordinates1[0]), parseFloat(ToOffsetCoordinates1[1])),
                //new google.maps.LatLng(parseFloat(ToCoordinates1[0]), parseFloat(ToCoordinates1[1])),
                //new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
                ];

                if (vessel2) {//If flightPath is already defined (already a polygon)
                    vessel2.setPath(bollardCoordinates1);
                }
                else {
                    vessel2 = new google.maps.Polygon({
                        paths: bollardCoordinates1,
                        strokeColor: '#000000',
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: '#000000',
                        fillOpacity: 0.5
                    });
                    //  var infowindow = new google.maps.InfoWindow({
                    //    content: toolTip,
                    //    suppressMapPan:true
                    //});
                    //eventPolygonClick = google.maps.event.addListener(vessel1, 'click', function (event) {
                    //    infowindow.setPosition(event.latLng);
                    //    infowindow.open(map, vessel1);
                    //});

                    //vessel2.pId = "state poly pId";


                    vessel2.setMap(map);
                }

                if (vessel.MovementStatus() == "CONF") {
                    vessel2.setOptions({ fillColor: '#006400', strokeColor: '#006400' });
                }
                else if (vessel.MovementStatus() == "SCH") {
                    vessel2.setOptions({ fillColor: '#FFBF00', strokeColor: '#FFBF00' });
                }
                else if (vessel.MovementStatus() == "BERT") {
                    vessel2.setOptions({ fillColor: '#CD6090', strokeColor: '#CD6090' });
                }
                vessel2.wPet = toolTip1;
                vessel2.infoWindow = new google.maps.InfoWindow();

                google.maps.event.addListener(vessel2, "mouseover", function (event) {

                    this.infoWindow.setContent(this.wPet);
                    this.infoWindow.open(map);
                    this.infoWindow.setPosition(event.latLng);

                });

                google.maps.event.addListener(vessel2, "mouseout", function () {
                    this.infoWindow.close();
                });

                map.panTo(bollardCoordinates1[bollardCoordinates1.length - 1]);
                map.setZoom(17);


            }
        }
        self.Initialize();

    }
    IPMSRoot.BerthPlanningGISViewModel = BerthPlanningGISViewModel;
    //IPMSRoot.berthplanninggisModel = BerthPlanningGISViewModel;

}(window.IPMSROOT));




//(function (IPMSRoot) {
//    var isView = 0;
//    var map;
//    var polyline1 = null;
//    var marker1 = null;
//    var markers1;

//    var BerthPlanningGISViewModel = function () {
//        var self = this;

//        var infowindow = [];
//        var marker = [];
//        var tooltip;
//        var vessel1;
//        var vessel2;

//        self.markers = ko.observableArray();

//        self.viewModelHelper = new IPMSROOT.viewModelHelper();
//        self.berthplanninggisModel = ko.observable(new IPMSRoot.BerthPlanningGISModel());
//        self.StartDate = ko.observable();
//        self.EndDate = ko.observable();
//        self.VesselsList = ko.observableArray();
//        self.quayberthsWithBollards = ko.observableArray();
//        self.PlannedVessels = ko.observableArray();
//        self.BerthedVessels = ko.observableArray();
//        self.gisMapPath = ko.observable();
//        //    self.PendingVessels = ko.observableArray();
//        self.Initialize = function () {
//            self.berthplanninggisModel(new IPMSROOT.BerthPlanningGISModel());
//            self.LoadVesselCallMovements();
//            self.LoadMap();
//            self.PlotBerthedVessels();
//            tooltip = new Opentip(
//              "div#GridContainer", //target element 
//              "DummyContent", // will be replaced
//              "", // title
//              {
//                  showOn: null,
//                  // I'll manually manage the showOn effect
//              });
//        }

//        self.VesselDetailsPop = function (data, event) {
//            var vesselinfo = getVesselInfo(data);
//            tooltip.setContent(vesselinfo);
//            tooltip.show();


//        }

//        function getVesselInfo(data) {
//            var data;
//            if ((data.MovementType() == "ARMV" || data.MovementType() == "SHMV" || data.MovementType() == "WRMV") && data.MovementStatus() == "BERT" || data.MovementStatus() == "SALD") {
//                data =
//                    "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardCode() + " to " + data.ToBollardCode() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.LOA() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Draft:</td><td>" + data.Draft() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
//                     "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Mooring Bollards:</td><td>" + data.MooringBowBollard() + " to " + data.MooringStemBollard() + "</td></tr>";
//            }
//            else {
//                data =
//                    "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardCode() + " to " + data.ToBollardCode() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.LOA() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Draft:</td><td>" + data.Draft() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
//                     "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
//                    "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>";
//            }
//            return data;
//        }

//        self.LoadMap = function () {

//            self.LoadMap = function () {

//                self.viewModelHelper.apiGet('api/GetGisMapPath', null,
//                    function (result) {
//                        self.gisMapPath(result);
//                    }, null, null, false);


//            google.maps.visualRefresh = true;
//            var Durban = new google.maps.LatLng(-29.873244000000000000, 31.024533000000020000);

//            // These are options that set initial zoom level, where the map is centered globally to start, and the type of map to show
//            var mapOptions = {
//                zoom: 15,
//                center: Durban,
//                mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
//            };

//            // This makes the div with id "map_canvas" a google map
//            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

//            var quayLayer = new google.maps.KmlLayer({

//                url: 'http://115.115.191.61:2226/images/KMZ/DBNquays.KMZ'
//            });

//            var berthLayer = new google.maps.KmlLayer({

//                url: 'http://115.115.191.61:2226/images/KMZ/DBNBerths.KMZ'
//            });


//            var bollardLayer = new google.maps.KmlLayer({

//                url: 'http://115.115.191.61:2226/images/KMZ/DBNBollards.KMZ'
//            });


//            quayLayer.setMap(map);
//            berthLayer.setMap(map);
//            bollardLayer.setMap(map);


//        }

//        self.LoadVesselCallMovements = function () {
//            self.StartDate(new Date());
//            var todate = new Date(self.StartDate());
//            self.EndDate(moment(todate).add('days', 1).format('MM-DD-YYYY'));

//            self.viewModelHelper.apiGet('api/GetVesselCallMovementsGIS/' + kendo.toString(self.StartDate(), "yyyy-MM-dd") + '/' + kendo.toString(self.EndDate(), "yyyy-MM-dd"), null,
//                 function (result) {
//                     $.each(result, function (index, item) {

//                         self.PlannedVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
//                         self.VesselsList.push(new IPMSRoot.BerthPlanningGISModel(item));
//                         //if (item.MovementStatus == "MPEN" || item.MovementStatus == null) {
//                         //       self.PendingVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
//                         //}
//                         //else {
//                         //console.log('VCN' ,  item.VCN +'/'+item.MovementStatus);
//                         //if (item.MovementStatus != "MPEN" && item.MovementStatus != "SALD")
//                         //{
//                         //    self.PlannedVessels.push(new IPMSRoot.BerthPlanningGISModel(item));
//                         //    self.VesselsList.push(new IPMSRoot.BerthPlanningGISModel(item));
//                         //}
//                         //}


//                     });



//                 },
//                null, null, false);

//        }


//        self.PlotBerthedVessels = function () {
//            $.each(self.PlannedVessels(), function (index, plannedvessel) {
//                if (plannedvessel.MovementStatus() == "BERT") {
//                    console.log('berthedvessel', plannedvessel);
//                    self.PlotVessel(plannedvessel);
//                }

//            });

//        }


//        self.PlotVessel = function (vessel) {

//            if (vessel.FromCoordinates() != null && vessel.ToCoordinates() != null && vessel.precoord() != null && vessel.preoffsetcord() != null) {
//                var Coordinates = vessel.FromCoordinates().split(',');
//                var toolTip = getVesselInfo(vessel);
//                var image = '../Content/Images/vesselbert.png';

//                var myLatLng = new google.maps.LatLng(parseFloat(Coordinates[0]), parseFloat(Coordinates[1]));

//                var FromCoordinates = vessel.FromCoordinates().split(',');
//                var ToCoordinates = vessel.ToCoordinates().split(',');
//                var FromOffsetCoordinates = vessel.FromOffsetCoordinates().split(',');
//                var ToOffsetCoordinates = vessel.ToOffsetCoordinates().split(',');
//                var FromMidCoordinates = vessel.FromMidCoordinates().split(',');
//                var ToMidCoordinates = vessel.ToMidCoordinates().split(',');
//                var preCoordinates = vessel.precoord().split(',');
//                var preOffsetCoordinates = vessel.preoffsetcord().split(',');
//                var bollardCoordinates = [
//                new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
//                new google.maps.LatLng(parseFloat(FromOffsetCoordinates[0]), parseFloat(FromOffsetCoordinates[1])),
//                new google.maps.LatLng(parseFloat(preOffsetCoordinates[0]), parseFloat(preOffsetCoordinates[1])),
//                new google.maps.LatLng(parseFloat(ToMidCoordinates[0]), parseFloat(ToMidCoordinates[1])),
//                new google.maps.LatLng(parseFloat(preCoordinates[0]), parseFloat(preCoordinates[1])),
//                new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
//                //new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
//                //new google.maps.LatLng(parseFloat(FromOffsetCoordinates[0]), parseFloat(FromOffsetCoordinates[1])),
//                //new google.maps.LatLng(parseFloat(ToOffsetCoordinates[0]), parseFloat(ToOffsetCoordinates[1])),
//                //new google.maps.LatLng(parseFloat(ToCoordinates[0]), parseFloat(ToCoordinates[1])),
//                //new google.maps.LatLng(parseFloat(FromCoordinates[0]), parseFloat(FromCoordinates[1])),
//                ];

//                var vessel1 = new google.maps.Polygon({
//                    paths: bollardCoordinates,
//                    strokeColor: '#FF69B4',
//                    strokeOpacity: 0.8,
//                    strokeWeight: 2,
//                    fillColor: '#FF69B4',
//                    fillOpacity: 0.5
//                });

//                //  var infowindow = new google.maps.InfoWindow({
//                //    content: toolTip,
//                //    suppressMapPan:true
//                //});
//                //eventPolygonClick = google.maps.event.addListener(vessel1, 'click', function (event) {
//                //    infowindow.setPosition(event.latLng);
//                //    infowindow.open(map, vessel1);
//                //});

//                //vessel1.pId = "state poly pId";
//                vessel1.wPet = toolTip;
//                vessel1.infoWindow = new google.maps.InfoWindow();

//                google.maps.event.addListener(vessel1, "mouseover", function (event) {

//                    this.infoWindow.setContent(this.wPet);
//                    this.infoWindow.open(map);
//                    this.infoWindow.setPosition(event.latLng);

//                });

//                google.maps.event.addListener(vessel1, "mouseout", function () {
//                    this.infoWindow.close();
//                });

//                vessel1.setMap(map);
//            }

//        }


//        self.MouseOut = function (data, event) {
//            tooltip.hide();
//        }



//        self.VesselClick = function (e) {
//            var vessel = e;

//            if (vessel.FromCoordinates() != null && vessel.ToCoordinates() != null && vessel.precoord() != null && vessel.preoffsetcord() != null) {
//                var Coordinates1 = vessel.FromCoordinates().split(',');
//                var toolTip1 = getVesselInfo(vessel);

//                var myLatLng = new google.maps.LatLng(parseFloat(Coordinates1[0]), parseFloat(Coordinates1[1]));

//                var bollardCoordinates1 = [];
//                var FromCoordinates1 = vessel.FromCoordinates().split(',');
//                var ToCoordinates1 = vessel.ToCoordinates().split(',');
//                var FromOffsetCoordinates1 = vessel.FromOffsetCoordinates().split(',');
//                var ToOffsetCoordinates1 = vessel.ToOffsetCoordinates().split(',');
//                var FromMidCoordinates1 = vessel.FromMidCoordinates().split(',');
//                var ToMidCoordinates1 = vessel.ToMidCoordinates().split(',');
//                var preCoordinates1 = vessel.precoord().split(',');
//                var preOffsetCoordinates1 = vessel.preoffsetcord().split(',');
//                var bollardCoordinates1 = [
//                new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
//                new google.maps.LatLng(parseFloat(FromOffsetCoordinates1[0]), parseFloat(FromOffsetCoordinates1[1])),
//                new google.maps.LatLng(parseFloat(preOffsetCoordinates1[0]), parseFloat(preOffsetCoordinates1[1])),
//                new google.maps.LatLng(parseFloat(ToMidCoordinates1[0]), parseFloat(ToMidCoordinates1[1])),
//                new google.maps.LatLng(parseFloat(preCoordinates1[0]), parseFloat(preCoordinates1[1])),
//                new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
//                //new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
//                //new google.maps.LatLng(parseFloat(FromOffsetCoordinates1[0]), parseFloat(FromOffsetCoordinates1[1])),
//                //new google.maps.LatLng(parseFloat(ToOffsetCoordinates1[0]), parseFloat(ToOffsetCoordinates1[1])),
//                //new google.maps.LatLng(parseFloat(ToCoordinates1[0]), parseFloat(ToCoordinates1[1])),
//                //new google.maps.LatLng(parseFloat(FromCoordinates1[0]), parseFloat(FromCoordinates1[1])),
//                ];

//                if (vessel2) {//If flightPath is already defined (already a polygon)
//                    vessel2.setPath(bollardCoordinates1);
//                }
//                else {
//                    vessel2 = new google.maps.Polygon({
//                        paths: bollardCoordinates1,
//                        strokeColor: '#000000',
//                        strokeOpacity: 0.8,
//                        strokeWeight: 2,
//                        fillColor: '#000000',
//                        fillOpacity: 0.5
//                    });



//                    //  var infowindow = new google.maps.InfoWindow({
//                    //    content: toolTip,
//                    //    suppressMapPan:true
//                    //});
//                    //eventPolygonClick = google.maps.event.addListener(vessel1, 'click', function (event) {
//                    //    infowindow.setPosition(event.latLng);
//                    //    infowindow.open(map, vessel1);
//                    //});

//                    //vessel2.pId = "state poly pId";


//                    vessel2.setMap(map);
//                }

//                if (vessel.MovementStatus() == "CONF") {
//                    vessel2.setOptions({ fillColor: '#006400', strokeColor: '#006400' });
//                }
//                else if (vessel.MovementStatus() == "SCH") {
//                    vessel2.setOptions({ fillColor: '#FFBF00', strokeColor: '#FFBF00' });
//                }
//                else if (vessel.MovementStatus() == "BERT") {
//                    vessel2.setOptions({ fillColor: '#CD6090', strokeColor: '#CD6090' });
//                }
//                vessel2.wPet = toolTip1;
//                vessel2.infoWindow = new google.maps.InfoWindow();

//                google.maps.event.addListener(vessel2, "mouseover", function (event) {

//                    this.infoWindow.setContent(this.wPet);
//                    this.infoWindow.open(map);
//                    this.infoWindow.setPosition(event.latLng);

//                });

//                google.maps.event.addListener(vessel2, "mouseout", function () {
//                    this.infoWindow.close();
//                });

//                map.panTo(bollardCoordinates1[bollardCoordinates1.length - 1]);
//                map.setZoom(17);


//            }
//        }
//        self.Initialize();

//    }
//    IPMSRoot.BerthPlanningGISViewModel = BerthPlanningGISViewModel;

//}(window.IPMSROOT));





