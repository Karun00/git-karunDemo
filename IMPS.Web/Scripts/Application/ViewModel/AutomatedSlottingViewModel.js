var _gridContainerWidth;
var _gridContainerHeight;
(function (IPMSRoot) {
    var AutomatedSlottingViewModel = function () {
        var self = this;

        self.IsExtendDisabled = ko.observable(false);
        var startX;
        var startY;
        var dragStartWidth;
        var dragStartHeight;

        var stageWidth;
        var stageHeight;

        //leftGrid Properties
        var LeftGridWidth;
        var LeftGridHeight;
        var leftGridCellWidth;
        var LeftGridLineCount;
        var LeftGridCellHeight;
        var RightGridWidth;
        var RightGridHeight;
        var GridGap;
        var RightLayerLines;

        var leftGridHeightz;
        var rightGridHeightz;


        // var TimeInterval = 3;
        var NoOfExtendRightGridShapes;
        var NoOfVesselsPerSlotInRightGrid;
        var NoOfVesselsPerSlotInLeftGrid;
        var RightGridCellHeight = 70;

        var afterExtended = 0;
        var layer;
        var leftGrid;
        var rightGrid;
        var leftLayerGroup;
        var rightLayerGroup;
        var leftRectangleText;
        var rightRectangleText;
        var leftRectangle;
        var rightRectangle;
        var maxvesselcount;

        var rightLayerGroupExtend;
        var rightRectangleExtend;
        //for left layer and right layers
        var leftheaderlayer;
        var leftLayer;
        var leftBackground;
        var leftHeaderBackground;
        var vesselmovementText;
        var leftlayertooltip;
        var TimerText;
        //var leftLayerGroup;
        var leftgridLine;
        var rightLayerTitleText;
        var leftLayerTitleText;
        var leftLayerWidthPlusGapWidth;
        var layerText;
        var bl;

        var TimeInterval;
        var nodays = 1;

        var noOftimerLines;

        // var noOftimerLines = nodays * 24 / TimeInterval;
        //noOftimerLines = noOftimerLines + 1;

        var tooltip;
        var currentDate = new Date();
        var hour = 0;
        var LayersGap = parseFloat(195.5);

        var AutomatedconfigurationYn = 'Y';

        var ActualUnPlannedArrivalVesselCount;
        var ActualUnPlannedShiftingVesselCount;
        var ActualUnPlannedSailingVesselCount;
        var ActualUnPlannedWarpingVesselCount;

        var isExtendableYn = 'N';

        //var rect2;


        _gridContainerWidth = $("#container").width();
        _gridContainerHeight = $("#container").height();

        self.GridContainerWidth = ko.observable(1272);//(_gridContainerWidth);
        self.GridContainerHeight = ko.observable(parseInt(NoOfVesselsPerSlotInRightGrid * RightGridCellHeight)); //ko.observable(_gridContainerHeight);
        self.CurrentDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });;

        self.PrevDayCount = ko.observable(-1);
        self.NextDayCount = ko.observable(1);
        self.isVisible = ko.observable(true);


        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.AutomatedSlotModel = ko.observable();

        self.ConfigurationDetails = ko.observableArray([]);

        self.GettingExtendableYesNo = ko.observableArray();

        self.AutomatedSlots = ko.observableArray([]);
        self.VesselMovementTypes = ko.observableArray();
        self.Plannedmovements = ko.observableArray();
        self.UnPlannedmovements = ko.observableArray();
        self.MovementTypeCode = ko.observableArray();
        self.ExtendYn = ko.observableArray();
        self.FromPositionPortCode = ko.observableArray();



        self.UnPlannedVesselArrivalDet = ko.observableArray();
        self.UnPlannedVesselSailingDet = ko.observableArray();
        self.UnPlannedVesselShiftingDet = ko.observableArray();
        self.UnPlannedVesselWarpingDet = ko.observableArray();

        self.maxCountVesselsPerMovement = ko.observableArray();

        self.rightLayerShapesArray = ko.observableArray();

        self.filterProducts = ko.observableArray();

        self.LoadUnPlannedVesselDet = function () {
            self.viewModelHelper.apiGet('api/UnPlannedVesselsDet/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {

                 if (result.length > 0) {
                     $.each(result, function (index, data) {

                         switch (data.MovementTypeCode) {
                             case "ARMV"://Arrival
                                 self.UnPlannedVesselArrivalDet.push(new IPMSRoot.VesselInfoModel(data));
                                 break;

                             case "SGMV"://Sailing
                                 self.UnPlannedVesselSailingDet.push(new IPMSRoot.VesselInfoModel(data));
                                 break;

                             case "SHMV"://Shifting
                                 self.UnPlannedVesselShiftingDet.push(new IPMSRoot.VesselInfoModel(data));
                                 break;

                             case "WRMV"://Warping
                                 self.UnPlannedVesselWarpingDet.push(new IPMSRoot.VesselInfoModel(data));
                                 break;
                         }



                     });
                 }
                 else {

                     if (layer != undefined) {
                         layer.get('.leftRectangle').each(function (shape, n) {
                             shape.remove();
                             layer.draw();
                         });
                     }

                     self.UnPlannedVesselArrivalDet([]);
                     self.UnPlannedVesselSailingDet([]);
                     self.UnPlannedVesselShiftingDet([]);
                     self.UnPlannedVesselWarpingDet([]);
                     return;
                 }

                 //ko.mapping.fromJS(result, {}, self.UnPlannedVesselDet);

             }, null, null, false);


            var maxCountVesselsInMovement = ko.observableArray([{ count: self.UnPlannedVesselArrivalDet().length }, { count: self.UnPlannedVesselSailingDet().length }, { count: self.UnPlannedVesselShiftingDet().length }, { count: self.UnPlannedVesselWarpingDet().length }]);
            //00
            maxVesselCount = ko.utils.arrayFirst(maxCountVesselsInMovement(), function (result) {
                return result.count === Math.max.apply(null, ko.utils.arrayMap(maxCountVesselsInMovement(), function (e) {
                    return e.count;
                }));

            });
            ActualUnPlannedArrivalVesselCount = self.UnPlannedVesselArrivalDet().length;
            ActualUnPlannedSailingVesselCount = self.UnPlannedVesselSailingDet().length;
            ActualUnPlannedShiftingVesselCount = self.UnPlannedVesselShiftingDet().length;
            ActualUnPlannedWarpingVesselCount = self.UnPlannedVesselWarpingDet().length;

            maxVesselCount = maxVesselCount.count;
        }

        self.LoadVesselMovementTypes = function () {
            self.viewModelHelper.apiGet('api/VesselMovementTypes',
           null,
             function (result) {
                 ko.mapping.fromJS(result, {}, self.VesselMovementTypes);
             }, null, null, false);
        }

        self.AutomatedConfigurationDetails = function () {

            self.viewModelHelper.apiGet('api/ConfigurationDetails/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {

                 if (result.length != 0) {
                     // self.ConfigurationDetails.push(new IPMSRoot.ConfigurationDetailsModel(result));
                     NoOfVesselsPerSlotInRightGrid = result[0].NoofSlots;
                     NoOfVesselsPerSlotInLeftGrid = result[0].NoofSlots;
                     TimeInterval = result[0].Duration;
                     NoOfExtendRightGridShapes = result[0].ExtendableSlots;

                     noOftimerLines = nodays * 24 / TimeInterval;
                     noOftimerLines = noOftimerLines + 1;
                 }
                 else {
                     AutomatedconfigurationYn = 'N';
                 }



             }, null, null, false);
        }

        self.AutomatedExtendableYesNo = function () {

            self.viewModelHelper.apiGet('api/GettingExtendableYesNo/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {

                 if (result == null) {
                     self.ExtendYn();
                     // isExtendableYn = result.ExtendYn;
                     isExtendableYn = 'Y';
                     self.ExtendYn('Y');
                 }
                 else {
                     isExtendableYn = 'N';
                     self.ExtendYn('N');
                 }


             }, null, null, false);
        }

        self.LoadPlannedSlotDetails = function () {

            self.viewModelHelper.apiGet('api/AutomatedSlots/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {
                 if (result.length > 0) {
                     $.each(result, function (index, data) {

                         self.AutomatedSlots.push(new IPMSRoot.VesselInfoModel(data));
                         //ko.mapping.fromJS(result, {}, self.AutomatedSlots);
                     });
                 } else {
                     self.AutomatedSlots([]);
                 }
             }, null, null, false);

        }

        var LoadUnPlannedShapeForArrival = function () {

            var x = leftGrid.cellWidth * 0;
            var y = leftGrid.cellHeight;
            var incY = 0;


            $.each(self.UnPlannedVesselArrivalDet(), function (index, data) {

                var shape = new Shape(data, x, incY);


                leftLayerGroup.add(leftRectangle);
                leftLayerGroup.add(leftRectangleText);

                layer.add(leftLayerGroup);
                stage.add(layer);
                layer.draw();

                incY = incY + y;

            });



        }

        var LoadUnPlannedShapeForSailing = function () {
            var x = leftGrid.cellWidth * 1;
            var y = leftGrid.cellHeight;
            var incY = 0;
            $.each(self.UnPlannedVesselSailingDet(), function (index, data) {
                var shape = new Shape(data, x, incY);

                leftLayerGroup.add(leftRectangle);
                leftLayerGroup.add(leftRectangleText);

                layer.add(leftLayerGroup);
                stage.add(layer);
                layer.draw();

                incY = incY + y;
            });
        }

        var LoadUnPlannedShapeForShifting = function () {
            var x = leftGrid.cellWidth * 2;
            var y = leftGrid.cellHeight;
            var incY = 0;

            $.each(self.UnPlannedVesselShiftingDet(), function (index, data) {

                var shape = new Shape(data, x, incY);

                leftLayerGroup.add(leftRectangle);
                leftLayerGroup.add(leftRectangleText);

                layer.add(leftLayerGroup);
                stage.add(layer);
                layer.draw();

                incY = incY + y;
            });
        }

        var LoadUnPlannedShapeForWarping = function () {
            var x = leftGrid.cellWidth * 3;
            var y = leftGrid.cellHeight;
            var incY = 0;

            $.each(self.UnPlannedVesselWarpingDet(), function (index, data) {

                var shape = new Shape(data, x, incY);

                leftLayerGroup.add(leftRectangle);
                leftLayerGroup.add(leftRectangleText);

                layer.add(leftLayerGroup);
                stage.add(layer);
                layer.draw();

                incY = incY + y;
            });
        }

        var LoadConfigurationDetails = function () {
        }

        var LoadPlannedSahpes = function () {

            if (isExtendableYn == 'Y') {
                // document.getElementById('btnConfirm').style.display = 'none';

            }

            if (self.AutomatedSlots().length == 0) {
                layer.get('.rightRectangle').each(function (shape, n) {
                    shape.remove();
                    layer.draw();
                });
                return;
            } else {
                GetRightlayerPossitions(layer, rightGrid, leftGrid, self.AutomatedSlots());
            }

        }

        //rightgridpopulating


        function Shape(data, x, incY) {
            if (data == undefined)
                return;

            var self = this;
            this.VCN = ko.observable(data.VCN());

            this.VesselInfo = ko.observable(data);
            this.TimeSlot = ko.observable();


            leftLayerGroup = new Kinetic.Group({
                x: x,
                y: incY + 60,
                width: leftGrid.cellWidth,
                height: leftGrid.cellHeight,
                draggable: true,
                name: 'leftGroup'

                //, dragBoundFunc: function (pos) {
                //    var newY = pos.y < LeftGridWidth ? LeftGridWidth : pos.y;
                //     return {
                //         x: pos.x,
                //         y: pos.y
                //     };
                // }
            });

            // make a rect that snaps to the grid
            leftRectangle = new Kinetic.Rect({
                x: 0,
                y: 0,
                width: leftGrid.cellWidth,
                height: leftGrid.cellHeight,
                fill: leftGridCellColor,
                stroke: 'black',
                strokeWidth: 2,
                name: 'leftRectangle'
                //draggable: true

            });

            leftRectangleText = new Kinetic.Text({
                //  y:25,
                text: data.VCN(),
                width: leftGrid.cellWidth,
                fontSize: 10,
                fontFamily: 'Calibri',
                textFill: 'blue',
                align: 'center',
                fill: 'white',
                padding: 5,
                name: 'leftRectangle',
                id: data.VesselCallMovementID()

            });


            leftLayerGroup.on('dragstart', function (e) {
                startX = this.attrs.x;
                startY = this.attrs.y;
                dragStartWidth = this.attrs.width;
                dragStartHeight = this.attrs.height;

            });

            leftLayerGroup.on("mouseover", function (event) {
                tooltip.setContent(data.ToolTip());
                tooltip.currentPosition.left = event.layerX;
                tooltip.currentPosition.top = event.layerY;
                tooltip.show();
            });

            leftLayerGroup.on("mouseout", function (event) {
                tooltip.hide();
            });
            //++           
            leftLayerGroup.on("dragend", function () {

                var endX = this.attrs.x;
                var endY = this.attrs.y;

                var llb = parseFloat(LeftGridWidth + rightGrid.cellWidth);
                var rightLayerEndX = parseFloat(endX - parseFloat(LeftGridWidth));
                var rightLayerEndY = parseFloat(endY);


                var LongLineNumberX = Math.round(parseFloat(rightLayerEndX) / parseFloat(rightGrid.cellWidth));
                var LongLineNumberY = Math.round(parseFloat(rightLayerEndY) / parseFloat(rightGrid.cellHeight));


                if (startX < LeftGridWidth && endX > LeftGridWidth && parseInt(endY + RightGridCellHeight) >= rightGridHeightz) {

                    LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(leftGrid.cellHeight)) - 1;

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * leftGrid.cellHeight) + 60;


                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;


                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;
                    this.children[0].attrs.fill = leftGridCellColor;


                    var layer = this.getLayer();
                    layer.draw();
                }


                else if (LongLineNumberX > 0) {

                    if (this.children[0].attrs.name != 'rightRectangle') {

                        var movementTypeForToMoveLeftGrid = self.VesselInfo().MovementTypeCode();

                        if (movementTypeForToMoveLeftGrid == 'ARMV') {

                            ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount - 1;
                        }
                        else if (movementTypeForToMoveLeftGrid == 'SGMV') {

                            ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount - 1;
                        }
                        else if (movementTypeForToMoveLeftGrid == 'SHMV') {

                            ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount - 1;

                        }
                        else if (movementTypeForToMoveLeftGrid == 'WRMV') {

                            ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount - 1;

                        }
                    }
                    var tempEndX = parseFloat(LongLineNumberX * rightGrid.cellWidth) - rightGrid.cellWidth;
                    tempEndX = parseFloat(tempEndX + llb);

                    // self.TimeSlot(LongLineNumberY * TimeInterval);
                    self.TimeSlot(parseFloat(parseFloat(LongLineNumberX - 1) * TimeInterval));

                    endX = tempEndX;

                    LongLineNumberY = LongLineNumberY;

                    var snapX = endX;
                    var snapY = parseFloat(parseFloat(LongLineNumberY) * rightGrid.cellHeight) + 60;


                    this.setPosition({
                        x: snapX,
                        y: snapY
                    });

                    this.attrs.width = rightGrid.cellWidth;
                    this.attrs.height = rightGrid.cellHeight;


                    this.children[0].attrs.width = rightGrid.cellWidth;
                    this.children[0].attrs.height = rightGrid.cellHeight;


                    // self.VesselInfo().PossitionX(snapX);
                    // self.VesselInfo().PossitionY(snapY);
                    // self.VesselInfo().SlotStatus('PLND');



                    self.VesselInfo().VCN(this.children[1].partialText);
                    self.VesselInfo().VesselCallMovementID(this.children[1].id());
                    self.VesselInfo().PossitionX(snapX);
                    self.VesselInfo().PossitionY(snapY);
                    self.VesselInfo().SlotStatus('PLND');

                    this.children[0].attrs.name = 'rightRectangle';

                    var tt = self.TimeSlot();

                    if (parseInt(self.TimeSlot()) <= 9)
                        self.TimeSlot('0' + self.TimeSlot());

                    var endT = parseFloat(tt + TimeInterval);
                    if (parseFloat(endT) <= 9)
                        endT = '0' + endT;

                    // }
                    self.VesselInfo().Slot(self.TimeSlot() + ' - ' + endT);

                    self.VesselInfo().shapeWidth(rightGrid.cellWidth);
                    self.VesselInfo().shapeHeight(rightGrid.cellHeight);

                    // Checking over lapping
                    var iscollide = VesselPlannedMovements(self.VesselInfo());
                    var LeftLongLineNumberX;
                    var LeftLongLineNumberY;


                    if (!iscollide) {

                        if (startX <= LeftGridWidth) {
                            LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                            LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(leftGrid.cellHeight)) - 1;

                            startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;
                            startY = parseFloat(parseFloat(LeftLongLineNumberY) * leftGrid.cellHeight) + 60;


                            this.setPosition({
                                x: startX,
                                y: startY
                            });

                            this.attrs.width = dragStartWidth;
                            this.attrs.height = dragStartHeight;

                            this.children[0].attrs.width = dragStartWidth;
                            this.children[0].attrs.height = dragStartHeight;
                            this.children[0].attrs.fill = leftGridCellColor;

                        }
                    }
                }
                else if (endX <= LeftGridWidth && startX > LeftGridWidth) {

                    var leftGridLineNumber;
                    var leftGridPositionY;

                    var movementTypeForToMoveLeftGrid = self.VesselInfo().MovementTypeCode();

                    if (movementTypeForToMoveLeftGrid == 'ARMV') {
                        leftGridLineNumber = 0;
                        leftGridPositionY = ActualUnPlannedArrivalVesselCount;
                        ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SGMV') {
                        leftGridLineNumber = 1;
                        leftGridPositionY = ActualUnPlannedSailingVesselCount;
                        ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SHMV') {
                        leftGridLineNumber = 2;

                        leftGridPositionY = ActualUnPlannedShiftingVesselCount;
                        ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount + 1;

                    }
                    else if (movementTypeForToMoveLeftGrid == 'WRMV') {
                        leftGridLineNumber = 3;
                        leftGridPositionY = ActualUnPlannedWarpingVesselCount;
                        ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount + 1;

                    }


                    var possitionX = leftGrid.cellWidth * leftGridLineNumber;
                    var possitionY = RightGridCellHeight * leftGridPositionY;
                    possitionY = parseFloat(possitionY + 60);

                    this.setPosition({
                        x: possitionX,
                        y: possitionY
                    });

                    this.attrs.width = leftGrid.cellWidth;
                    this.attrs.height = RightGridCellHeight;

                    this.children[0].attrs.width = leftGrid.cellWidth;
                    this.children[0].attrs.height = RightGridCellHeight;
                    this.children[0].attrs.fill = leftGridCellColor;


                }

                else if (startX >= LeftGridWidth) {


                    LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));


                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * rightGrid.cellHeight) + 60;

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;

                    //  this.children[0].position({ x: startX, y: startY });
                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;


                    //this.position({ x: startX, y: startY });
                    //this.attrs.width = dragStartWidth;
                    //this.attrs.height = dragStartHeight;

                }


                else if (startX >= LeftGridWidth) {


                    var leftGridLineNumber;
                    var leftGridPositionY;

                    var movementTypeForToMoveLeftGrid = self.VesselInfo().MovementTypeCode();

                    if (movementTypeForToMoveLeftGrid == 'ARMV') {
                        leftGridLineNumber = 0;
                        leftGridPositionY = ActualUnPlannedArrivalVesselCount;
                        ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SGMV') {
                        leftGridLineNumber = 1;
                        leftGridPositionY = ActualUnPlannedSailingVesselCount;
                        ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SHMV') {
                        leftGridLineNumber = 2;

                        leftGridPositionY = ActualUnPlannedShiftingVesselCount;
                        ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount + 1;

                    }
                    else if (movementTypeForToMoveLeftGrid == 'WRMV') {
                        leftGridLineNumber = 3;
                        leftGridPositionY = ActualUnPlannedWarpingVesselCount;
                        ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount + 1;

                    }


                    var possitionX = leftGrid.cellWidth * leftGridLineNumber;
                    var possitionY = RightGridCellHeight * leftGridPositionY;
                    possitionY = parseFloat(possitionY + 60);

                    this.setPosition({
                        x: possitionX,
                        y: possitionY
                    });

                    this.attrs.width = leftGrid.cellWidth;
                    this.attrs.height = RightGridCellHeight;

                    this.children[0].attrs.width = leftGrid.cellWidth;
                    this.children[0].attrs.height = RightGridCellHeight;
                    this.children[0].attrs.fill = leftGridCellColor;

                    // LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                    // LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));

                    // startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;
                    // startY = parseFloat(parseFloat(LeftLongLineNumberY) * rightGrid.cellHeight) + 60;


                    // this.setPosition({
                    //     x: startX,
                    //     y: startY
                    // });

                    // this.attrs.width = dragStartWidth;
                    // this.attrs.height = dragStartHeight;
                    //// this.children[0].position({ x: startX, y: startY });
                    // this.children[0].attrs.x = startX;
                    // this.children[0].attrs.y = startY;

                    // this.children[0].attrs.width = dragStartWidth;
                    // this.children[0].attrs.height = dragStartHeight;


                    //this.position({ x: startX, y: startY });
                    //this.attrs.width = dragStartWidth;
                    //this.attrs.height = dragStartHeight;

                }
                else {

                    LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(leftGrid.cellHeight)) - 1;

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * leftGrid.cellHeight) + 60;

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;

                    // this.children[0].position({ x: startX, y: this.attrs.y });
                    //this.children[0].attrs.x = startX;
                    //this.children[0].attrs.y = startY-60;

                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;
                    this.children[0].attrs.fill = leftGridCellColor;

                    //this.position({ x: startX, y: startY });
                    //this.attrs.width = dragStartWidth;
                    //this.attrs.height = dragStartHeight;
                    //this.attrs.fill = leftGridCellColor;
                }
                var layer = this.getLayer();

                layer.draw();


            });

        }

        var changedPlannedMovements = function (data) {

            var match = false;
            var isCollide = false;

            //var itemClone;
            //itemClone = ko.observables(data);
            //var vesselsperSlot = ko.utils.arrayFilter(itemClone, function (item) {

            //    
            //    return (
            //        item.VesselCallMovementID() === itemClone.VesselCallMovementID(), item.VCN() === itemClone.VCN(), item.SlotDate() === itemClone.SlotDate(), item.Slot() === itemClone.Slot(),
            //        item.PossitionX() === itemClone.PossitionX(), item.PossitionY() === itemClone.PossitionY(), item.shapeWidth() === itemClone.shapeWidth(), item.shapeHeight() === itemClone.shapeHeight()
            //);
            //});

            data.SlotDate(self.CurrentDate());

            $.each(self.AutomatedSlots(), function (index, value) {

                if (value != undefined) {
                    self.AutomatedSlots()[index].PossitionX(value.PossitionX());
                    self.AutomatedSlots()[index].PossitionX(value.PossitionY());
                    self.AutomatedSlots()[index].shapeWidth(value.shapeWidth());
                    self.AutomatedSlots()[index].shapeHeight(value.shapeHeight());

                    if (value != undefined && data != undefined) {
                        if (value.VesselCallMovementID() == data.VesselCallMovementID()) {

                            self.AutomatedSlots.remove(data);
                            match = true;
                        }
                    }
                }
            });

            //$.each(self.AutomatedSlots(), function (index, value) {
            //    if (value.VCN() == data.VCN()) {
            //        match = true;
            //    }
            //});
            // Here restricting  same Vessel again not adding in to Plannedmovements list 


            // if (!match) {

            self.AutomatedSlots.push(data);
            //  }

            //$.each(self.Plannedmovements(), function (index, item) {

            for (var i = 0; i <= self.AutomatedSlots().length - 1 ; i++) {

                if (self.AutomatedSlots().length >= 1) {
                    var item = self.AutomatedSlots()[i];
                    if (item.VesselCallMovementID() != data.VesselCallMovementID()) {


                        isCollide = theyAreColliding(data, item)
                        //isCollide = doObjectsCollide(data, VCN, width, height, x, y)
                        if (isCollide) {
                            // self.AutomatedSlots.remove(data);
                            return true;
                            break;
                        }

                    }
                }
            }
            //});

            return isCollide;
        }

        //var VesselPlannedMovements = function (data,VCN,width,height,x,y) {
        var VesselPlannedMovements = function (data) {

            var match = false;
            var isCollide = false;

            //var itemClone;
            //itemClone = ko.observables(data);
            //var vesselsperSlot = ko.utils.arrayFilter(itemClone, function (item) {

            //    
            //    return (
            //        item.VesselCallMovementID() === itemClone.VesselCallMovementID(), item.VCN() === itemClone.VCN(), item.SlotDate() === itemClone.SlotDate(), item.Slot() === itemClone.Slot(),
            //        item.PossitionX() === itemClone.PossitionX(), item.PossitionY() === itemClone.PossitionY(), item.shapeWidth() === itemClone.shapeWidth(), item.shapeHeight() === itemClone.shapeHeight()
            //);
            //});

            data.SlotDate(self.CurrentDate());

            $.each(self.AutomatedSlots(), function (index, value) {

                if (value != undefined) {
                    self.AutomatedSlots()[index].PossitionX(value.PossitionX());
                    self.AutomatedSlots()[index].PossitionX(value.PossitionY());
                    self.AutomatedSlots()[index].shapeWidth(value.shapeWidth());
                    self.AutomatedSlots()[index].shapeHeight(value.shapeHeight());

                    if (value != undefined && data != undefined) {
                        if (value.VesselCallMovementID() == data.VesselCallMovementID()) {

                            self.AutomatedSlots.remove(data);
                            match = true;
                        }
                    }
                }
            });

            //$.each(self.AutomatedSlots(), function (index, value) {
            //    if (value.VCN() == data.VCN()) {
            //        match = true;
            //    }
            //});
            // Here restricting  same Vessel again not adding in to Plannedmovements list 


            // if (!match) {

            self.AutomatedSlots.push(data);
            //  }

            //$.each(self.Plannedmovements(), function (index, item) {

            for (var i = 0; i <= self.AutomatedSlots().length - 1 ; i++) {

                if (self.AutomatedSlots().length >= 1) {
                    var item = self.AutomatedSlots()[i];
                    if (item.VesselCallMovementID() != data.VesselCallMovementID()) {


                        isCollide = theyAreColliding(data, item)
                        //isCollide = doObjectsCollide(data, VCN, width, height, x, y)
                        if (isCollide) {
                            // self.AutomatedSlots.remove(data);
                            return true;
                            break;
                        }

                    }
                }
            }

            return isCollide;
        }

        function InitializeStages() {
            stage = new Kinetic.Stage({
                container: "container",
                width: self.GridContainerWidth(),
                height: 700
            });
            stageWidth = stage.attrs.width;
            stageHeight = stage.attrs.height;
            $("#container").height(stageHeight);

            LeftGridWidth = parseFloat(stageWidth) / 4;
            LeftGridHeight = parseInt(RightGridCellHeight * NoOfVesselsPerSlotInRightGrid);
            leftGridHeightz = parseInt(RightGridCellHeight * NoOfVesselsPerSlotInRightGrid);

            RightGridWidth = parseFloat(stageWidth * 3) / 4;
            RightGridHeight = parseInt(RightGridCellHeight * NoOfVesselsPerSlotInRightGrid);
            rightGridHeightz = parseInt(RightGridCellHeight * NoOfVesselsPerSlotInRightGrid);

            // RightGridCellHeight = stageHeight / NoOfVesselsPerSlotInRightGrid;
            RightGridCellHeight = 70;

            leftGridCellColor = "red";

            layer = new Kinetic.Layer();
            stage.add(layer);


            // make a left grid
            leftGrid = new Kinetic.Shape({
                sceneFunc: function (ctx) {
                    var lineCount = this.lineCount;
                    var xSpan = this.cellWidth;
                    var ySpan = this.cellHeight;
                    var cw = LeftGridWidth;
                    var ch = parseInt(NoOfVesselsPerSlotInLeftGrid * RightGridCellHeight);

                    ctx.beginPath();

                    var x;
                    var y;

                    for (var i = 0; i < lineCount; i++) {
                        x = (i) * xSpan;

                        ctx.moveTo(x, 0);
                        ctx.lineTo(x, ch);

                        //if (parseFloat(maxVesselCount.count) > parseFloat(NoOfVesselsPerSlotInRightGrid)) {
                        //    for (var j = 0; j < maxVesselCount.count; j++) {
                        //        y = (j) * ySpan;
                        //        ctx.moveTo(0, y);
                        //        ctx.lineTo(cw, y);
                        //    }
                        //}

                        //else {

                        for (var j = 0; j < parseInt(NoOfVesselsPerSlotInLeftGrid + 1) ; j++) {
                            y = (j) * ySpan;
                            ctx.moveTo(0, y);
                            ctx.lineTo(cw, y);
                        }
                        //}

                    }

                    // KineticJS specific context method
                    ctx.fillStrokeShape(this);
                },
                x: 0,
                y: 60,
                stroke: 'black',
                strokeWidth: 2
            });
            leftGrid.lineCount = 5;
            leftGrid.cellWidth = LeftGridWidth / 4;
            leftGrid.cellHeight = RightGridCellHeight;
            //if (parseFloat(maxVesselCount.count) > parseFloat(NoOfVesselsPerSlotInRightGrid))
            //    leftGrid.cellHeight = LeftGridHeight / maxVesselCount.count;
            //else
            //    leftGrid.cellHeight = LeftGridHeight / NoOfVesselsPerSlotInRightGrid;



            layer.add(leftGrid);
            layer.draw();


            LeftGridLineCount = leftGrid.lineCount;
            leftGridCellWidth = leftGrid.cellWidth;
            LeftGridCellHeight = leftGrid.cellHeight;
            //+




            // make a right grid
            rightGrid = new Kinetic.Shape({
                sceneFunc: function (ctx) {

                    var lineCount = this.lineCount;
                    var xSpan = this.cellWidth;
                    var ySpan = this.cellHeight;
                    var cw = RightGridWidth;
                    var ch = parseInt(NoOfVesselsPerSlotInRightGrid * RightGridCellHeight);
                    ctx.beginPath();

                    for (var i = 0; i < lineCount; i++) {

                        var x = (i + 1) * xSpan;
                        var y = (i + 1) * ySpan;

                        ctx.moveTo(x, 0);
                        ctx.lineTo(x, ch);

                        for (var j = 0; j < NoOfVesselsPerSlotInRightGrid + 1; j++) {
                            y = (j) * ySpan;

                            ctx.moveTo(xSpan, y);
                            ctx.lineTo(cw, y);
                        }


                    }
                    // KineticJS specific context method
                    ctx.fillStrokeShape(this);
                },
                x: LeftGridWidth,
                y: 60,
                stroke: 'blue',
                strokeWidth: 2
            });

            rightGrid.lineCount = noOftimerLines;
            rightGrid.cellWidth = RightGridWidth / rightGrid.lineCount;
            rightGrid.cellHeight = RightGridCellHeight;



            layer.add(rightGrid);
            layer.draw();

            vesselmovementText = new Kinetic.Text({
                fontSize: 12,
                fontFamily: 'Calibri',
                fill: 'black',
                paddingleft: 2,
                align: 'center'
            });

            tooltip = new Opentip(
                    "div#container", //target element 
                    "DummyContent", // will be replaced
                    "", // title
                    {
                        showOn: null, // I'll manually manage the showOn effect
                    });


            var rightLayerHeaderWidth = RightGridWidth - rightGrid.cellWidth;

            rightLayer = new Kinetic.Layer({
                x: 0,
                y: 0,
                id: 'rightLayer',
                width: rightLayerHeaderWidth,
                height: 60
            });

            rightheaderlayer = new Kinetic.Layer({
                x: LayersGap,
                y: 0,
                id: 'rightLayer',
                width: rightLayerHeaderWidth,
                height: 20
            });



            rightBackground = new Kinetic.Rect({
                x: 0, y: 0,
                width: rightLayerHeaderWidth,
                height: 60,
                stroke: "#ffff",
                fill: 'ivory'
            });

            rightHeaderBackground = new Kinetic.Rect({
                x: LayersGap, y: 30,
                width: rightLayerHeaderWidth,
                height: 30,
                stroke: "#ffff",
                fill: 'orange'
            });
            //for right layer title
            rightLayerTitle = new Kinetic.Layer({
                x: LayersGap,
                y: 0,
                id: 'rightLayer'
            });

            rightLayerTitleText = new Kinetic.Text({
                x: LayersGap, y: 0,
                fontSize: 13,
                fontFamily: 'Calibri, sans-serif',
                fill: 'blue',
                align: 'top',
                text: 'Automated Slot Plan'
            });

            TimerText = new Kinetic.Text({
                x: 0, y: 40,
                fontSize: 13,
                fontFamily: 'Calibri, sans-serif',
                fill: '#CCC',
                paddingleft: 5,
                align: 'top'
            });


            rightheaderlayer.add(rightHeaderBackground);
            stage.add(rightheaderlayer);
            stage.add(rightLayer);
            stage.add(rightLayerTitle);

            ///Timer


            RightLayerLines = RightGridWidth / noOftimerLines;


            for (var i = 0; i < noOftimerLines; i++) {

                var H = (RightLayerLines * i);
                //Adding Vertical GridLines
                var gridShortTimerLine = new Kinetic.Line({
                    x: LayersGap,
                    y: 30,
                    points: [H, 0, H, 30],
                    stroke: 'red',
                    strokeWidth: .5,
                    fill: 'black',
                    closed: false
                });



                var sttime = hour + (i * TimeInterval);
                if (sttime >= 24 && sttime < 48) {
                    sttime = sttime - 24;
                }
                else if (sttime >= 48) {
                    sttime = sttime - 48;
                }

                var stndtime = sttime;
                if (sttime != undefined && sttime != null && sttime.toString().length > 0) {
                    if (parseFloat(sttime) <= 9)
                        sttime = '0' + sttime;
                }
                var ht = (i * RightLayerLines) + LayersGap + RightLayerLines / 2;

                if (TimeInterval != undefined && TimeInterval != null && TimeInterval.toString().length > 0) {
                    var divTime = parseFloat(stndtime) + parseFloat(TimeInterval)
                    if (parseFloat(divTime) <= 9)
                        divTime = '0' + divTime;

                    sttime = sttime + ' - ' + divTime;
                }
                rightheaderlayer.add(gridShortTimerLine);
                rightheaderlayer.add(addberthText(TimerText, ht, sttime));
                rightheaderlayer.add(vesselmovementText);
                rightheaderlayer.draw();

                rightLayer.draw();
                rightLayerTitle.add(rightLayerTitleText);
                rightLayerTitle.draw();


            }

            leftLayer = new Kinetic.Layer({
                x: 0,
                y: 30,
                id: 'leftLayer',
                width: LeftGridWidth,
                height: 60
            });

            leftheaderlayer = new Kinetic.Layer({
                x: 0,
                y: 30,
                id: 'leftLayer',
                width: LeftGridWidth,
                height: 30
            });

            leftBackground = new Kinetic.Rect({
                x: 0, y: 30,
                width: LeftGridWidth,
                height: 30,
                stroke: "#ffff",
                fill: 'ivory'
            });

            leftHeaderBackground = new Kinetic.Rect({
                x: 0, y: 0,
                width: LeftGridWidth,
                height: 30,
                stroke: "#ffff",
                fill: 'green'
            });


            leftLayerTitle = new Kinetic.Layer({
                x: 0,
                y: 0,
                id: 'leftLayer'
            });

            leftLayerTitleText = new Kinetic.Text({
                x: 0, y: 0,
                fontSize: 13,
                fontFamily: 'Calibri, sans-serif',
                fill: 'blue',
                align: 'top',
                text: 'Vessels waiting for slots'
            });



            leftheaderlayer.add(leftHeaderBackground);
            stage.add(leftheaderlayer);
            stage.add(leftLayer);
            stage.add(leftLayerTitle);


            var prevbl = 0;
            for (var i = 0; i <= self.VesselMovementTypes().length - 1 ; i++) {

                var movementtype = self.VesselMovementTypes()[i].SubCatName();

                bl = leftGrid.cellWidth

                var I = prevbl;
                var textdisplaydistance = 0;
                textdisplaydistance = prevbl + bl / 2;


                //Header
                var leftlayerheaderLine = new Kinetic.Line({
                    points: [I, 0, I, 30],
                    stroke: 'red',
                    strokeWidth: 2,
                    closed: false,
                });

                leftgridLine = new Kinetic.Line({
                    points: [I, 0, I, 30],
                    stroke: 'red',
                    strokeWidth: 2,
                    closed: false,
                });


                leftheaderlayer.add(leftlayerheaderLine);
                leftheaderlayer.add(addberthText(vesselmovementText, textdisplaydistance, movementtype));
                leftheaderlayer.add(vesselmovementText);
                leftheaderlayer.draw();

                leftLayer.add(leftgridLine);
                leftLayer.draw();
                leftLayerTitle.add(leftLayerTitleText);
                leftLayerTitle.draw();

                prevbl = prevbl + bl;
            }


        }

        self.Initialize = function (shapes) {
            self.AutomatedConfigurationDetails();
            self.AutomatedExtendableYesNo();



            if (AutomatedconfigurationYn == 'Y') {
                if (isExtendableYn == 'Y') {
                    NoOfVesselsPerSlotInRightGrid = parseInt(NoOfVesselsPerSlotInRightGrid) + parseInt(NoOfExtendRightGridShapes);
                    // document.getElementById('btnConfirm').style.display = 'none';

                }
                // LoadConfigurationDetails();
                self.LoadVesselMovementTypes();
                self.LoadUnPlannedVesselDet();
                self.LoadPlannedSlotDetails();

                InitializeStages();

                LoadUnPlannedShapeForArrival();
                LoadUnPlannedShapeForSailing();
                LoadUnPlannedShapeForShifting();
                LoadUnPlannedShapeForWarping();

                LoadPlannedSahpes();
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Automated Configuration is not configured, Please configure properly.", "Automated Slotting");
            }

            //LoadVesselAutomatedSlots();
        }

        self.ExtendRightGridShapes = function () {

            var self = this;
            //this.VCN = ko.observable(data.VCN());
            //this.VesselInfo = ko.observable(data);
            isExtendableYn = 'Y';

            if (parseInt(afterExtended) < parseInt(NoOfExtendRightGridShapes)) {

                layer.get('.rightRectangle').each(function (shape, n) {

                    shape.destroy();
                    layer.draw();
                });

                afterExtended = parseInt(NoOfVesselsPerSlotInRightGrid) + parseInt(NoOfExtendRightGridShapes);

                NoOfVesselsPerSlotInRightGrid = parseInt(NoOfVesselsPerSlotInRightGrid) + parseInt(NoOfExtendRightGridShapes);

                var afterExt = parseInt(afterExtended + 1);

                this.GridContainerHeight = parseInt(afterExt * RightGridCellHeight);
                this._gridContainerHeight = parseInt(afterExt * RightGridCellHeight);
                $("#container").height(parseInt(afterExt * RightGridCellHeight));
                stage.height = parseInt(afterExt * RightGridCellHeight);
                rightGrid.height = parseInt(afterExt * RightGridCellHeight);
                leftGrid.height = parseInt(afterExt * RightGridCellHeight);
                stage.attrs.height = parseInt(afterExt * RightGridCellHeight);

                rightGridHeightz = parseInt(afterExt * RightGridCellHeight);

                layer.add(rightGrid);
                layer.add(leftGrid);
                stage.add(layer);
                layer.draw();

                //self.LoadPlannedSlotDetails();
                // self.Initialize();
                LoadPlannedSahpes();


            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("You have already extended.!", "Automated Vessel Slotting");
            }
        }

        self.confirm = function (data) {
            self.viewModelHelper.apiPut('api/UpdateVesselSoltDtl', ko.mapping.toJSON(self.AutomatedSlots()), function Message(msg) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Vessel Slotting details updated successfully.", "Automated Vessel Slotting");
            });
        }

        self.PreviousDate = function () {
            // confirmation box - start
            $.confirm({
                'title': 'Automated Slotting',
                'message': 'All changes done to slot planning will be lost. Do you want proceed anyway?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.PrevDayCount(parseInt(self.PrevDayCount()));

                            var yesterday = self.CurrentDate();
                            yesterday.setDate(yesterday.getDate() + self.PrevDayCount());
                            var currentDateComp = new Date();

                            var month = currentDateComp.getMonth() + 1;
                            currentDateComp = month + "/" + currentDateComp.getDate() + "/" + currentDateComp.getFullYear();

                            var month = yesterday.getMonth() + 1;
                            var yesterdayNew = month + "/" + yesterday.getDate() + "/" + yesterday.getFullYear();

                            if (yesterdayNew >= currentDateComp) {
                                document.getElementById('btnConfirm').style.display = 'block';
                            }
                            else {
                                document.getElementById('btnConfirm').style.display = 'none';
                            }

                            self.CurrentDate(yesterday);
                            self.LoadUnPlannedVesselDet();
                            LoadUnPlannedShapeForArrival();
                            LoadUnPlannedShapeForSailing();
                            LoadUnPlannedShapeForShifting();
                            LoadUnPlannedShapeForWarping();

                            self.LoadPlannedSlotDetails();
                            LoadPlannedSahpes();
                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {
                        }
                    }
                }
            });
            //confirmation box - end
        }

        self.NextDate = function () {
            // confirmation box - start
            $.confirm({
                'title': 'Automated Slotting',
                'message': 'All changes done to slot planning will be lost. Do you want proceed anyway?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.NextDayCount(parseInt(self.NextDayCount()));

                            var nextdate = self.CurrentDate();
                            nextdate.setDate(nextdate.getDate() + self.NextDayCount());

                            self.CurrentDate(nextdate);

                            var currentDateComp = new Date();
                            var month = currentDateComp.getMonth() + 1;
                            currentDateComp = month + "/" + currentDateComp.getDate() + "/" + currentDateComp.getFullYear();

                            var month = nextdate.getMonth() + 1;
                            var nextdateNew = month + "/" + nextdate.getDate() + "/" + nextdate.getFullYear();

                            if (nextdateNew >= currentDateComp) {
                                document.getElementById('btnConfirm').style.display = 'block';
                            }
                            else {
                                document.getElementById('btnConfirm').style.display = 'none';
                            }

                            self.LoadUnPlannedVesselDet();
                            LoadUnPlannedShapeForArrival();
                            LoadUnPlannedShapeForSailing();
                            LoadUnPlannedShapeForShifting();
                            LoadUnPlannedShapeForWarping();
                            self.LoadPlannedSlotDetails();
                            LoadPlannedSahpes();
                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {
                        }
                    }
                }
            });
            //confirmation box - end
        }

        self.Initialize();

        function GetRightlayerPossitions(layer, rightGrid, leftGrid, data) {

            //RightLayerLines = RightGridWidth / noOftimerLines
            var vesselsperSlot;
            for (var i = 0; i < noOftimerLines - 1; i++) {

                var interval = GetTimeSlot(i, TimeInterval, hour, RightLayerLines, LayersGap);

                vesselsperSlot = ko.utils.arrayFilter(data, function (item) {

                    return item.Slot() == interval;
                });


                var incY = 0;

                for (var j = 0; j < vesselsperSlot.length; j++) {
                    if (j < NoOfVesselsPerSlotInRightGrid) {
                        var slots = interval.split('-');
                        var slotStartPosition = slots[0];

                        var vesselsperData = vesselsperSlot[j];

                        vesselsperData.ExtendYn(isExtendableYn);
                        vesselsperData.FromPositionPortCode(data[j].FromPositionPortCode());
                        _gridContainerWidth = $("#container").width();
                        _gridContainerHeight = $("#container").height();

                        LeftGridWidth = parseFloat(_gridContainerWidth) / 4;
                        LeftGridHeight = parseFloat(_gridContainerHeight);
                        RightGridWidth = parseFloat(_gridContainerWidth * 3) / 4;
                        RightGridHeight = parseFloat(_gridContainerHeight);

                        var possitionX = rightGrid.cellWidth * i;
                        possitionX = parseFloat(possitionX + LeftGridWidth + rightGrid.cellWidth);

                        var possitionY = rightGrid.cellHeight * j;
                        possitionY = parseFloat(possitionY + 60);

                        RightGridshapes(possitionX, possitionY, rightGrid, vesselsperSlot[j]);

                        rightLayerGroup.add(rightRectangle);
                        rightLayerGroup.add(rightRectangleText);

                        layer.add(rightLayerGroup);
                        stage.add(layer);
                        layer.draw();

                        //layer.add(rightRectangle);
                        //layer.draw();
                    }
                }
            }
        }

        function RightGridshapes(possitionX, possitionY, rightGrid, data) {

            var self = this;
            this.VCN = ko.observable(data.VCN());
            this.VesselInfo = ko.observable(data);
            this.TimeSlot = ko.observable();
            var isDraggable = true;

            var color;
            var IsDragable = true;
            switch (data.SlotStatus()) {
                case 'CMPL':
                    color = "#0000FF";
                    isDraggable = false;
                    break;
                case 'CNFR':
                    color = "#00FF00";
                    isDraggable = false;
                    break;
                case 'ONG':
                    color = "#37FAD6";
                    isDraggable = false;
                    break;
                case 'OVRD':
                    color = "#F6CECE";
                    break;
                case 'PLND':
                    color = "#FFBF00";
                    break;
            }

            rightLayerGroup = new Kinetic.Group({
                x: possitionX,
                y: possitionY,
                width: rightGrid.cellWidth,
                height: rightGrid.cellHeight,
                draggable: isDraggable,
                name: 'rightGridGroup'

                //, dragBoundFunc: function () {
                //     //var newY = pos.y < 50 ? 50 : pos.y;
                //     return {
                //         x: this.x,
                //         y: this.y
                //     };
                // }
            });


            rightRectangle = new Kinetic.Rect({

                width: rightGrid.cellWidth,
                height: rightGrid.cellHeight,
                fill: color,
                stroke: 'white',
                strokeWidth: 2,
                //   draggable: isDraggable,
                name: 'rightRectangle'

            });

            rightRectangleText = new Kinetic.Text({
                //y:25,
                text: data.VCN(),
                width: rightGrid.cellWidth,
                fontSize: 10,
                fontFamily: 'Calibri',
                textFill: 'blue',
                align: 'center',
                fill: 'white',
                padding: 5,
                name: 'rightRectangle',
                id: data.VesselCallMovementID()

            });


            rightLayerGroup.on("mouseover", function (event) {
                tooltip.setContent(data.ToolTip());
                tooltip.currentPosition.left = event.layerX;
                tooltip.currentPosition.top = event.layerY;
                tooltip.show();
            });

            rightLayerGroup.on("mouseout", function (event) {
                tooltip.hide();
            });

            rightLayerGroup.on('dragstart', function (e) {
                startX = this.attrs.x;
                startY = this.attrs.y;
                dragStartWidth = this.attrs.width;
                dragStartHeight = this.attrs.height;

            });

            rightLayerGroup.on("dragend", function () {


                var endX = this.attrs.x;
                var endY = this.attrs.y;

                var llb = parseFloat(LeftGridWidth + rightGrid.cellWidth);
                var rightLayerEndX = parseFloat(endX - parseFloat(LeftGridWidth));
                var rightLayerEndY = parseFloat(endY);


                var LongLineNumberX = Math.round(parseFloat(rightLayerEndX) / parseFloat(rightGrid.cellWidth));
                var LongLineNumberY = Math.round(parseFloat(rightLayerEndY) / parseFloat(rightGrid.cellHeight));


                if (endX > LeftGridWidth && parseInt(endY + (RightGridCellHeight / 4)) >= rightGridHeightz) {

                    LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;
                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * RightGridCellHeight) + 60;
                    startY = parseInt(startY - RightGridCellHeight);

                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;

                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    var layer = this.getLayer();
                    layer.draw();
                }
                else if (parseInt(endY + RightGridCellHeight) >= rightGridHeightz) {

                    LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(leftGrid.cellHeight)) - 1;

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * leftGrid.cellHeight) + 60;
                    //startY = parseInt(startY - RightGridCellHeight);

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;

                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;
                    this.children[0].attrs.fill = leftGridCellColor;


                    var layer = this.getLayer();
                    layer.draw();
                }

                else if (LongLineNumberX > 0) {

                    /*
                    var movementTypeForToMoveLeftGrid = self.VesselInfo().MovementTypeCode();

                    if (movementTypeForToMoveLeftGrid == 'ARMV') {

                        ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount - 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SGMV') {

                        ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount - 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SHMV') {

                        ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount - 1;

                    }
                    else if (movementTypeForToMoveLeftGrid == 'WRMV') {

                        ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount - 1;

                    }
                    */
                    var tempEndX = parseFloat(LongLineNumberX * rightGrid.cellWidth) - rightGrid.cellWidth;
                    tempEndX = parseFloat(tempEndX + llb);


                    self.TimeSlot(parseFloat(parseFloat(LongLineNumberX - 1) * TimeInterval));

                    endX = tempEndX;

                    LongLineNumberY = LongLineNumberY;

                    var snapX = endX;
                    var snapY = parseFloat(parseFloat(LongLineNumberY) * rightGrid.cellHeight) + 60;


                    this.setPosition({
                        x: snapX,
                        y: snapY
                    });

                    this.attrs.width = rightGrid.cellWidth;
                    this.attrs.height = rightGrid.cellHeight;


                    this.children[0].attrs.width = rightGrid.cellWidth;
                    this.children[0].attrs.height = rightGrid.cellHeight;

                    this.children[0].attrs.name = 'rightRectangle';

                    self.VesselInfo().VCN(this.children[1].partialText);
                    self.VesselInfo().VesselCallMovementID(this.children[1].id());
                    self.VesselInfo().PossitionX(snapX);
                    self.VesselInfo().PossitionY(snapY);
                    self.VesselInfo().SlotStatus('PLND');

                    //if (self.TimeSlot().length == 1) {

                    var tt = self.TimeSlot();

                    if (parseInt(self.TimeSlot()) <= 9)
                        self.TimeSlot('0' + self.TimeSlot());

                    var endT = parseFloat(tt + TimeInterval);
                    if (parseFloat(endT) <= 9)
                        endT = '0' + endT;

                    // }
                    self.VesselInfo().Slot(self.TimeSlot() + ' - ' + endT);

                    self.VesselInfo().shapeWidth(rightGrid.cellWidth);
                    self.VesselInfo().shapeHeight(rightGrid.cellHeight);


                    // Checking over lapping
                    //var iscollide = VesselPlannedMovements(self.VesselInfo(), this.children[1].partialText,this.children[0].attrs.width, this.children[0].attrs.height, endX, endY);
                    var iscollide = VesselPlannedMovements(self.VesselInfo());
                    changedPlannedMovements(self.VesselInfo(), self.VesselInfo().VCN());
                    var LeftLongLineNumberX;
                    var LeftLongLineNumberY;
                    if (iscollide) {

                        if (startX <= LeftGridWidth) {
                            LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                            LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(leftGrid.cellHeight)) - 1;

                            startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;
                            startY = parseFloat(parseFloat(LeftLongLineNumberY) * leftGrid.cellHeight) + 60;

                            this.setPosition({
                                x: startX,
                                y: startY
                            });

                            this.attrs.width = rightGrid.cellWidth;
                            this.attrs.height = rightGrid.cellHeight;

                            this.children[0].attrs.width = dragStartWidth;
                            this.children[0].attrs.height = dragStartHeight;
                            this.children[0].attrs.fill = leftGridCellColor;

                        }
                        else if (startX >= LeftGridWidth) {


                            LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                            LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));

                            startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;

                            startY = parseFloat(parseFloat(LeftLongLineNumberY) * rightGrid.cellHeight) + 60;

                            this.setPosition({
                                x: startX,
                                y: startY
                            });

                            this.attrs.width = dragStartWidth;
                            this.attrs.height = dragStartHeight;

                            this.children[0].attrs.width = dragStartWidth;
                            this.children[0].attrs.height = dragStartHeight;


                        }
                        var layer = this.getLayer();
                        layer.draw();
                    }

                }
                else if (endX <= LeftGridWidth && startX > LeftGridWidth) {


                    var leftGridLineNumber;
                    var leftGridPositionY;

                    var movementTypeForToMoveLeftGrid = self.VesselInfo().MovementTypeCode();

                    if (movementTypeForToMoveLeftGrid == 'ARMV') {
                        leftGridLineNumber = 0;
                        leftGridPositionY = ActualUnPlannedArrivalVesselCount;
                        ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SGMV') {
                        leftGridLineNumber = 1;
                        leftGridPositionY = ActualUnPlannedSailingVesselCount;
                        ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount + 1;
                    }
                    else if (movementTypeForToMoveLeftGrid == 'SHMV') {
                        leftGridLineNumber = 2;

                        leftGridPositionY = ActualUnPlannedShiftingVesselCount;
                        ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount + 1;

                    }
                    else if (movementTypeForToMoveLeftGrid == 'WRMV') {
                        leftGridLineNumber = 3;
                        leftGridPositionY = ActualUnPlannedWarpingVesselCount;
                        ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount + 1;

                    }

                    self.VesselInfo().PossitionX(snapX);
                    self.VesselInfo().PossitionY(snapY);
                    //self.VesselInfo().SlotStatus('PEDG'); PLEASE UNCOMMENT AFTER INSERTING PEDG IN SUBCATEGORY
                    self.VesselInfo().SlotStatus('PEND');

                    this.children[0].attrs.name = 'leftRectangle';

                    var possitionX = leftGrid.cellWidth * leftGridLineNumber;
                    var possitionY = RightGridCellHeight * leftGridPositionY;
                    possitionY = parseFloat(possitionY + 60);

                    this.setPosition({
                        x: possitionX,
                        y: possitionY
                    });

                    this.attrs.width = leftGrid.cellWidth;
                    this.attrs.height = RightGridCellHeight;

                    this.children[0].attrs.width = leftGrid.cellWidth;
                    this.children[0].attrs.height = RightGridCellHeight;
                    this.children[0].attrs.fill = leftGridCellColor;

                    /*

                    LeftLongLineNumberX = Math.round(parseFloat(endX) / parseFloat(leftGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(endY) / parseFloat(leftGrid.cellHeight)) - 1;

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * RightGridCellHeight) + 60;
                    //startY = parseInt(startY - RightGridCellHeight);

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    this.attrs.width = leftGrid.cellWidth;
                    this.attrs.height = RightGridCellHeight;

                    // this.children[0].position({ x: startX, y: this.attrs.y });
                    //this.children[0].attrs.x = startX;
                    //this.children[0].attrs.y = startY-60;

                    this.children[0].attrs.width = leftGrid.cellWidth;
                    this.children[0].attrs.height = RightGridCellHeight;
                    this.children[0].attrs.fill = leftGridCellColor;


                    //var layer = this.getLayer();
                    //layer.draw();

                   
                    
                    //LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                    //LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));

                    //startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;
                    //startY = parseFloat(parseFloat(LeftLongLineNumberY) * rightGrid.cellHeight) + 60;

                    //this.attrs.width = dragStartWidth;
                    //this.attrs.height = dragStartHeight;
                 
                    //this.children[0].attrs.width = dragStartWidth;
                    //this.children[0].attrs.height = dragStartHeight;
                    */


                }
                else {


                    LeftLongLineNumberX = Math.round(parseFloat(startX - (LeftGridWidth + rightGrid.cellWidth)) / parseFloat(rightGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(rightGrid.cellHeight));

                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * rightGrid.cellWidth) + LeftGridWidth;

                    startY = parseFloat(parseFloat(LeftLongLineNumberY - 1) * rightGrid.cellHeight) + 60;

                    this.setPosition({
                        x: startX,
                        y: startY
                    });


                    /*
                    LeftLongLineNumberX = Math.round(parseFloat(startX) / parseFloat(leftGrid.cellWidth)) + 1;
                    LeftLongLineNumberY = Math.round(parseFloat(startY) / parseFloat(RightGridCellHeight)) - 1;
                    var ff = startX;
                    startX = parseFloat(parseFloat(LeftLongLineNumberX) * leftGrid.cellWidth) - leftGrid.cellWidth;
                    startY = parseFloat(parseFloat(LeftLongLineNumberY) * RightGridCellHeight) + 60;

                    this.setPosition({
                        x: startX,
                        y: startY
                    });

                    ff = ff - startX;
                    alert(ff);
                    this.attrs.width = dragStartWidth;
                    this.attrs.height = dragStartHeight;
                   
                  //  this.children[0].position({ x: startX, y: startY });
                    this.children[0].attrs.width = dragStartWidth;
                    this.children[0].attrs.height = dragStartHeight;
                    */
                    // this.children[0].attrs.fill = leftGridCellColor;

                    //  this.attrs.draggable = true;
                    //  this.children[0].attrs.draggable = true;

                    //this.position({ x: startX, y: startY });
                    //this.attrs.width = dragStartWidth;
                    //this.attrs.height = dragStartHeight;
                    //this.attrs.fill = leftGridCellColor;

                }

                var layer = this.getLayer();
                layer.draw();
            });
        }

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
    }

    IPMSRoot.AutomatedSlottingViewModel = AutomatedSlottingViewModel;
}(window.IPMSROOT));


var doObjectsCollide = function (rect1, VCN, width, height, x, y) {

    var isCollide = false;
    return !((rect1.PossitionX() - rect1.shapeWidth() >= x + width &&
       rect1.PossitionY() - rect1.shapeHeight() >= y + height &&
       rect1.PossitionX() + rect1.shapeWidth() <= x + width &&
       rect1.PossitionX() + rect1.shapeHeight() <= y - height));
    //return !( (rect1.PossitionX() - rect1.shapeWidth() >= rect2.x + rect2.shapeWidth() &&
    //    rect1.PossitionY() - rect1.shapeHeight() >= rect2.PossitionY() + rect2.shapeHeight() &&
    //    rect1.PossitionX() + rect1.shapeWidth() <= rect2.PossitionX() + rect2.shapeWidth() &&
    //    rect1.PossitionX() + rect1.shapeHeight() <= rect2.PossitionY() - rect2.shapeHeight()));

}

function isRectCollide(rect1, rect2) { // a and rect2 are your objects
    return !(
     ((rect1.PossitionY() + rect1.shapeHeight()) < (rect2.PossitionY())) ||
     (rect1.PossitionY() > (rect2.y + rect2.shapeHeight())) ||
     ((rect1.PossitionX() + rect1.shapeWidth()) < rect2.PossitionX()) ||
     (rect1.PossitionX() > (rect2.PossitionX() + rect2.shapeWidth()))
    );
}


function addberthText(control, index, text) {

    control = control.clone({ text: text, x: index });
    control.offsetX(control.height() / 2);
    return control;
}

function theyAreColliding(rect1, rect2) {
    var isCollide = false;
    //var leftGridCellWidth = leftGrid.cellWidth;


    //if (!(rect2.PossitionX() > rect1.PossitionX() + rect1.shapeWidth() || //
    //rect2.PossitionX() + rect1.shapeWidth() < rect1.PossitionX() || // 
    //rect2.possitionY() > rect1.possitionY() + rect2.shapeWidth() || //
    //rect2.possitionY() + rect2.shapeWidth() < rect1.possitionY())) {
    //    return true;
    //}
    //else {

    //    return false;
    //}
}


//function theyAreColliding(rect1, rect2) {
//    
//    var isCollide = false;

//    if (!(rect2.PossitionX() >= parseFloat(rect1.PossitionX() + rect1.shapeWidth()) || parseFloat(rect2.PossitionX() + rect2.shapeWidth()) < rect1.PossitionX() ||
//          rect2.PossitionY() > parseFloat(rect1.PossitionY() + rect1.shapeHeight()) || parseFloat(rect2.PossitionY() + rect2.shapeHeight()) < rect1.PossitionY())) {
//        return true;
//    }
//}

function GetTimeSlot(i, TimeInterval, hour, RightLayerLines, LayersGap) {

    var sttime = hour + (i * TimeInterval);
    if (sttime >= 24 && sttime < 48) {
        sttime = sttime - 24;
    }
    else if (sttime >= 48) {
        sttime = sttime - 48;
    }

    var stndtime = sttime;
    if (sttime != undefined && sttime != null && sttime.toString().length > 0) {
        if (parseFloat(sttime) <= 9)
            sttime = '0' + sttime;
    }

    var ht = (i * RightLayerLines) + LayersGap + RightLayerLines / 2;
    if (TimeInterval != undefined && TimeInterval != null && TimeInterval.toString().length > 0) {
        var divTime = parseFloat(stndtime) + parseFloat(TimeInterval)
        if (parseFloat(divTime) <= 9)
            divTime = '0' + divTime;
        sttime = sttime + ' - ' + divTime;
    }

    return sttime;
}

/*
function GetRightlayerPossitions(layer, leftRectangle, rightGrid, slot, leftGrid, j, rightGridLongLineNumber) {

    var slots = slot.split('-');
    var slotStartPosition = slots[0];
    // alert(noOftimerLines);
    if (slotStartPosition.length >= 2) {
        slotStartPosition = parseFloat(rightGridLongLineNumber);

    }

    _gridContainerWidth = $("#container").width();
    _gridContainerHeight = $("#container").height();

    LeftGridWidth = parseFloat(_gridContainerWidth) / 4;
    LeftGridHeight = parseFloat(_gridContainerHeight);
    RightGridWidth = parseFloat(_gridContainerWidth * 3) / 4;
    RightGridHeight = parseFloat(_gridContainerHeight);



    var possitionX = rightGrid.cellWidth * slotStartPosition;
    possitionX = parseFloat(possitionX + LeftGridWidth + rightGrid.cellWidth);
    var possitionY = rightGrid.cellHeight * (j - 1);
    possitionY = parseFloat(possitionY + 60);

    leftRectangle = leftRectangle.clone({

        x: possitionX,
        y: possitionY,
        width: rightGrid.cellWidth,
        height: rightGrid.cellHeight,
        fill: 'orange',
        stroke: 'black',
        strokeWidth: 2,
        draggable: true,
        name: 'rightRectangle'

    });



    layer.add(leftRectangle);
    layer.draw();

}


var LoadVesselAutomatedSlots = function () {

            if (self.AutomatedSlots().length == 0) {
                layer.get('.rightRectangle').each(function (shape, n) {
                    shape.remove();
    layer.draw();
                });
                return;
            }

            for (var i = 0; i < noOftimerLines; i++) {

                var interval = GetTimeSlot(i, TimeInterval, hour, RightLayerLines, LayersGap);

                var vesselbyslot = ko.utils.arrayMap(self.AutomatedSlots(), function (item) {
                    return item.Slot();
                });

                var movementTypeForToMoveLeftGrid = ko.utils.arrayMap(self.AutomatedSlots(), function (item) {
                    return item.MovementType();
                });

                var slots = vesselbyslot.filter(function (x) { return x === interval; });
                movementTypeForToMoveLeftGrid = movementTypeForToMoveLeftGrid[i];
                rightGridLongLineNumber = i;a

                for (var j = 1; j <= slots.length; j++) {

                    //if (parseFloat(j - 1) <= NoOfVesselsPerSlotInRightGrid) {
                    var value = slots[0];
                    var data = self.AutomatedSlots()[j];

                    GetRightlayerPossitions(layer, leftRectangle, rightGrid, value, leftGrid, j, rightGridLongLineNumber);

                    leftRectangle.on("mouseover", function (event) {
                        tooltip.setContent(data.ToolTip());
                        tooltip.currentPosition.left = event.layerX;
                        tooltip.currentPosition.top = event.layerY;
                        tooltip.show();
                    });

                    leftRectangle.on("mouseout", function (event) {
                        tooltip.hide();
                    });
                    //}

                    //else {

                    //var leftGridLineNumber;
                    //var leftGridPositionY;

                    //if (movementTypeForToMoveLeftGrid == 'Arrival') {
                    //    leftGridLineNumber = 0;
                    //    leftGridPositionY = ActualUnPlannedArrivalVesselCount;
                    //    ActualUnPlannedArrivalVesselCount = ActualUnPlannedArrivalVesselCount + 1;
                    //}
                    //else if (movementTypeForToMoveLeftGrid == 'Sailing') {
                    //    leftGridLineNumber = 1;
                    //    leftGridPositionY = ActualUnPlannedSailingVesselCount;
                    //    ActualUnPlannedSailingVesselCount = ActualUnPlannedSailingVesselCount + 1;
                    //}
                    //else if (movementTypeForToMoveLeftGrid == 'Shifting') {
                    //    leftGridLineNumber = 2;

                    //    leftGridPositionY = ActualUnPlannedShiftingVesselCount;
                    //    ActualUnPlannedShiftingVesselCount = ActualUnPlannedShiftingVesselCount + 1;

                    //}
                    //else if (movementTypeForToMoveLeftGrid == 'Warping') {
                    //    leftGridLineNumber = 3;
                    //    leftGridPositionY = ActualUnPlannedWarpingVesselCount;
                    //    ActualUnPlannedWarpingVesselCount = ActualUnPlannedWarpingVesselCount + 1;

                    //}


                    //var possitionX = leftGrid.cellWidth * leftGridLineNumber;
                    //var possitionY = leftGrid.cellHeight * leftGridPositionY;
                    //possitionY = parseFloat(possitionY + 60);


                    //rect1 = rect1.clone({
                    //    x: possitionX,
                    //    y: possitionY,
                    //    width: leftGrid.cellWidth,
                    //    height: leftGrid.cellHeight,
                    //    fill: 'green',
                    //    stroke: 'black',
                    //    strokeWidth: 2,
                    //    draggable: true

                    //});

                    //layer.add(rect1);
                    //layer.draw();
                    //}
                }
}
        }


*/



