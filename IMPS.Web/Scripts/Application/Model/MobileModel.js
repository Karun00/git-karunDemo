var MobileBerthModel = function (data) {
    var self = this;
    self.Berths = ko.observableArray();
    self.selectedQuayKey = ko.observable("");
    self.selectedQuayKey.subscribe(function (key) {
        if (key !== undefined) {
            var quaySelected = data.filter(function (item) {
                return item.QuayKey() === key;
            })[0];
            self.Berths(ko.toJS(quaySelected.Berths));
        }
    });

    self.selectedBerth = ko.observable();
    self.selectedBerthKey = ko.observable("");
    self.selectedBerthKey.subscribe(function (key) {
        if (key !== undefined) {
            var berthSelected = self.Berths().filter(function (item) {
                return item.BerthKey === key;
            })[0];
            self.selectedBerth(ko.toJS(berthSelected));
        }
        else {
            self.selectedBerth("");
        }
    });
}

//Mobile Berth Model
var Berth = function (data) {
    var self = this;
    self.PortCode = ko.observable(data ? data.PortCode : "");
    self.QueyCode = ko.observable(data ? data.QueyCode : "");
    self.BerthCode = ko.observable(data ? data.BerthCode : "");
    self.BerthName = ko.observable(data ? data.BerthName : "");
    self.BerthKey = ko.observable(data ? data.BerthKey : "");
    self.Draftm = ko.observable(data ? data.Draftm : "");
    self.Lengthm = ko.observable(data ? data.Lengthm : "");
    self.CargoDetails = ko.observable(data ? data.CargoDetails : "");
    self.TidalDraft = ko.observable(data ? data.TidalDraft : "");
    self.FirstBolardName = ko.observable(data ? data.FirstBolardName : "");
    self.LastBollardName = ko.observable(data ? data.LastBollardName : "");
    self.Bollards = ko.observableArray(data ? (data.Bollards ? $.map(data.Bollards, function (item) { return new Bollard(item) }) : []) : []);
}

var Bollard = function (data) {
    var self = this;
    self.BollardName = ko.observable(data ? data.BollardName : "");
    self.BolardKey = ko.observable(data ? data.BolardKey : "");
}

//Mobile Quay Model
var Quay = function (data) {
    var self = this;
    self.PortCode = ko.observable(data ? data.PortCode : "");
    self.QuayCode = ko.observable(data ? data.QuayCode : "");
    self.QuayName = ko.observable(data ? data.QuayName : "");
    self.QuayKey = ko.observable(data ? data.QuayKey : "");
    self.Berths = ko.observableArray(data ? (data.berthlist ? $.map(data.berthlist, function (item) { return new Berth(item) }) : []) : []);
}

////////////////////////////////Pending Notification Starts Here

var NotificationModel = function (data) {
    var self = this;

    self.WorkflowTaskCode = ko.observable();
    self.EntityCode = ko.observable();
    self.PageUrl = ko.observable();
    self.EntityName = ko.observable(data ? data.EntityName : "");
    self.group = ko.observable();
    self.group.subscribe(function (value) {
        value.dataSource.group([{ field: "EntityName" }]);
    });

    self.NextStep = ko.observable();
    self.SubCatName = ko.observable(data ? data.SubCatName : "");
    self.TaskID = ko.observable(data ? data.TaskID : "");
    self.columnNames = ko.observableArray([]);
    self.EntityColumns = ko.observable(data ? data.EntityColumns : "");
    if (data != undefined) {
        if (data.EntityColumns != null) {
            var column = data.EntityColumns.split(',');
            for (var i = 0; i < column.length; i++) {
                self.columnNames.push({ Column: column[i] });

            }
        }
        self.columnNames.push({ Column: "Workflow Status" });
        self.columnNames.push({ Column: "Remarks" });
        self.columnNames.push({ Column: "Action" });
    }

    self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
        return new pendingTask(task, data.PageUrl, self.EntityColumns(), self.SubCatName(), data.RequestName);
    }) : []);

}

var pendingTask = function (data, objpage, entitycolumn, workflowstatus, requestname) {
    var strHash = document.location.pathname;
    var value = strHash.substring(strHash.lastIndexOf('/') + 1);

    var self = this;
    self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
    self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
    self.Remarks = ko.observable(data ? data.Remarks : "");
    self.ReferenceData = ko.observable(data ? data.ReferenceData : "");
    self.Rows = ko.observableArray([]);

    self.HasRemarks = ko.observable(data ? data.HasRemarks : "");

    self.TaskCode = ko.observable(data ? data.TaskCode : "");
    self.APIUrl = ko.observable(data ? data.APIUrl : "");
    self.TaskName = ko.observable(data ? data.TaskName : "");
    self.GetTask = ko.observableArray([]);

    if (data != undefined) {
        actualObj = jQuery.parseJSON(data.ReferenceData);
        if (entitycolumn != null) {
            // var column = entitycolumn.replace(/\s/g, '');
            var column = entitycolumn.split(',');
            $.each(column, function (k, v) {

                $.each(actualObj, function (key, value) {
                    if (v.replace(/\s/g, '') == key) {
                        self.Rows.push({ Row: "<b>" + v + " </b> :  " + "<span style=font-weight:normal!important;>" + value + "</span>" });
                    }
                });
            });

            var rl = column.length - self.Rows().length;
            for (var i = 0; i < rl; i++) {
                self.Rows.push({ Row: "" });
            }
        }

        self.Rows.push({ Row: " <b>Workflow Status</b>  :  " + workflowstatus });
        self.Rows.push({ Row: "RowRemarks" });
        self.Rows.push({ Row: "RowAction", ReferenceID: data.ReferenceID, PageUrl: objpage });
        if (data.APIUrl != null) {
            var tc = data.TaskCode.split(',');
            var action = data.APIUrl.split(',');
            var taskname = data.TaskName.split(',');
            var hasremarks = data.HasRemarks.split(',');
            for (var i = 0; i < tc.length; i++) {
                self.GetTask.push({ TaskCode: tc[i], TaskName: taskname[i], APIUrl: action[i], Remarks: data.Remarks, WorkflowInstanceId: data.WorkflowInstanceId, ReferenceID: data.ReferenceID, RequestName: requestname, HasRemarks: hasremarks[i] });
                //var url = "/" + objpage + "/" + data.ReferenceID
                //if (value == data.ReferenceID && strHash == url)
                //    self.GetViewList.push({ TaskCode: tc[i], TaskName: taskname[i], APIUrl: action[i], Remarks: data.Remarks, WorkflowInstanceId: data.WorkflowInstanceId, ReferenceID: data.ReferenceID });
            };
        }


    }


}
//var Conversation = function (data) {
//    var self = this;
//    self.UserID2 = ko.observable(data ? data.UserID : "");
//}

//    //Mobile conversation reply model
//var ConversationReply = function (data) {
//    var self = this;
//    self.ConversationID = ko.observable(data ? data.ConversationID : "");
//    self.Reply = ko.observable(data ? data.Reply : "");
//    self.ConversationReplyID = ko.observable(data ? data.Reply : "");
//    self.IsRead = ko.observable(data ? data.Reply : "");
//}



//function GetDateTime() {
//    var date = new Date();
//    var month = date.getMonth() + 1;
//    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
//    return currentDate;
//}

var LabelValueModel = function (label, value) {
    var self = this;
    self.label = ko.observable(label);
    self.value = ko.observable(value);
}


///////////////////////////////Pending Notification Ends Here

// MobileConversationModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
var MobileConversationModel = function (data) {
    var self = this;
    self.UserID = ko.observable(data ? data.UserID : "");
    self.UserName = ko.observable(data ? data.UserName : "");
    self.FirstName = ko.observable(data ? data.FirstName : "");
    self.ConversationID = ko.observable(data ? data.ConversationID : "");
    self.selectedChoice = ko.observable();

    self.cache = function () { };
    self.set(data);
}

// Mobile conversation model
var Conversation = function (data) {
    var self = this;
    self.UserID2 = ko.observable(data ? data.UserID : "");
}

//Mobile conversation reply model
var ConversationReply = function (data) {
    var self = this;
    self.ConversationID = ko.observable(data ? data.ConversationID : "");
    self.Reply = ko.observable(data ? data.Reply : "");
    self.ConversationReplyID = ko.observable(data ? data.ConversationReplyID : "");
    self.IsRead = ko.observable(data ? data.IsRead : "");
}

var ScheduledTaskExecutionModel = function (data) {

    var self = this;

    self.OperationType = ko.observable(data ? data.OperationType : "");
    self.MomentType = ko.observable(data ? data.MomentType : "");
    self.VCNName = ko.observable(data ? data.VCNName : "");
    self.VesselName = ko.observable(data ? data.VesselName : "");
    self.VCN = ko.observable(data ? data.VCN : "");
    self.RecPKID = ko.observable(data ? data.RecPKID : "");
    self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
    self.StartTime = ko.observable(data ? data.StartTime : "");
    self.PilotOnBoard = ko.observable(data ? data.PilotOnBoard : "");
    self.PilotOff = ko.observable(data ? data.PilotOff : "");
    self.EndTime = ko.observable(data ? data.EndTime : "");
    self.LineUp = ko.observable(data ? data.LineUp : "");
    self.LineDown = ko.observable(data ? data.LineDown : "");
    self.WaitingStartTime = ko.observable(data ? data.WaitingStartTime : "");
    self.WaitingEndTime = ko.observable(data ? data.WaitingEndTime : "");
    self.AdditionalTugs = ko.observable(data ? data.AdditionalTugs : "");
    self.OffSteam = ko.observable(data ? (data.OffSteam == 'Y' ? true : false) : "");
    self.MarineRevenueCleared = ko.observable(data ? (data.MarineRevenueCleared == 'Y' ? true : false) : "");
    self.Remarks = ko.observable(data ? data.Remarks : "");
    self.DelayReason = ko.observable(data ? data.DelayReason : "");
    self.Deficiencies = ko.observable(data ? data.Deficiencies : "");
    self.FieldValue = ko.observable(data ? data.FieldValue : "");
    self.FieldName = ko.observable(data ? data.FieldName : "");
    self.ValPKID = ko.observable(data ? data.ValPKID : "");
    self.Berths = ko.observableArray();
    self.Bollards = ko.observableArray();
    self.FromBolardKey = ko.observable(data ? data.FromBolardKey : "");
    self.MooringBolardBowKey = ko.observable(data ? data.MooringBolardBowKey : "");
    self.ToBolardKey = ko.observable(data ? data.ToBolardKey : "");
    self.MooringBolardStemKey = ko.observable(data ? data.MooringBolardStemKey : "");
    self.FromBerthKey = ko.observable(data ? data.FromBerthKey : "");
    self.FromBetrth = ko.observable(data ? data.FromBetrth : "");
    self.ToBerthKey = ko.observable(data ? data.ToBerthKey : "");
    self.BerthingSide = ko.observable(data ? data.BerthingSide : "");
    self.MeterSerialNo = ko.observable(data ? data.MeterSerialNo : "");
    self.TotalDispensed = ko.observable(data ? data.TotalDispensed : "");
    self.ClosingMeterReading = ko.observable(data ? data.ClosingMeterReading : "");
    self.OpeningMeterReading = ko.observable(data ? data.OpeningMeterReading : "");
    self.TimeAlongSide = ko.observable(data ? data.TimeAlongSide : "");
    self.FirstSwing = ko.observable(data ? data.FirstSwing : "");
    self.LastSwing = ko.observable(data ? data.LastSwing : "");
    self.FirstLineIn = ko.observable(data ? data.FirstLineIn : "");
    self.LastLineIn = ko.observable(data ? data.LastLineIn : "");
    self.FirstLineOut = ko.observable(data ? data.FirstLineOut : "");
    self.LastLineOut = ko.observable(data ? data.LastLineOut : "");
    self.ForwardDraft = ko.observable(data ? (data.ForwardDraft != null ? data.ForwardDraft : "") : "");
    self.AftDraft = ko.observable(data ? (data.AftDraft != null ? data.AftDraft : "") : "");
    self.VesselLength = ko.observable(data ? (data.VesselLength != null ? data.VesselLength : "") : "");
    self.BerthName = ko.observable(data ? data.BerthName : "");
    self.BerthKey = ko.observable(data ? data.BerthKey : "");
    self.IsExecute = ko.observable(data ? data.IsExecute : "");
    self.BackToQuay = ko.observable(data ? data.BackToQuay : "");
    self.Extend = ko.observable(data ? (data.Extend == 'Y' ? true : false) : "");
    //if (self.StartTime != null) {
    //    $("#divStartTime").children().prop('disabled', true);
    //}
    //if (self.PilotOnBoard != null) {
    //    $("#divPilotOnBoard").children().prop('disabled', true);
    //}
    //if (self.PilotOff != null) {
    //    $("#divPilotOff").children().prop('disabled', true);
    //}
    //if (self.EndTime != null) {
    //    $("#divEndTime").children().prop('disabled', true);
    //}
    //if (self.WaitingStartTime != null) {
    //    $("#divWaitingStartTime").children().prop('disabled', true);
    //}
    //if (self.WaitingEndTime != null) {
    //    $("#divWaitingEndTime").children().prop('disabled', true);
    //}

    if (self.OperationType() == 'BRTH') {
        $("#btnStrtTime").removeClass('btn-disable');
        $("#btnStrtTime").addClass('km-button');
    }

}
///Report an Incident starts here

// MobileIncidentReportModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
var MobileIncidentReportModel = function (data) {
    var self = this;
    self.incidentLocation = ko.observable("").extend({ required: { message: '* Incident Location is required' } });
    self.incidentDescription = ko.observable("").extend({ required: { message: '* Incident Description is Required' } });
    self.UploadedFiles = ko.observableArray([]);
    self.IncedentTypeArray = ko.observableArray([]);
    self.IncidentDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentDocuments, function (incidentDocument) {
        return new IncidentDocumnet(incidentDocument);
    }) : []);
    self.IncidentNatures = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentNatures, function (incidentNature) {
        return new IncidentNature(incidentNature);
    }) : []);
}

// Mobile IncidentDocumnet model
var IncidentDocumnet = function (data) {
    var self = this;
    self.IncidentID = ko.observable(data ? data.IncidentID : "");
    self.DocumentID = ko.observable(data ? data.DocumentID : "");
    self.DocumentType = ko.observable(data ? data.DocumentType : "");
    self.DocumentName = ko.observable(data ? data.DocumentName : "");
    self.FileName = ko.observable(data ? data.FileName : "");
    self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
}

//Mobile IncidentNature model
var IncidentNature = function (data) {
    var self = this;
    self.IncidentNatureID = ko.observable(data ? data.IncidentNatureID : "");
    self.IncidentID = ko.observable(data ? data.IncidentID : "");
}

// IncidentReportReferenceData is used to get the different incident types

// Mobile IncidentType model
var IncidentType = function (data) {
    var self = this;
    self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
    self.SubCatName = ko.observable(data ? data.SubCatName : "");
}

function allowOnlyTwoPositiveDigtsOnly(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    if (dotPos == -1 && charCode != 46 && number[0].length > 1) {
        return false;
    }
    if (number[0].length > 1 && dotPos == 2 && caratPos <= dotPos) {
        return false;
    }
    return true;
}

//thanks: http://javascript.nwbox.com/cursor_position/
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

function allowOnlyThreePositiveDigts(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 2)) {
        return false;
    }
    return true;
}

//thanks: http://javascript.nwbox.com/cursor_position/
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}


/// Report an Incident ends here
