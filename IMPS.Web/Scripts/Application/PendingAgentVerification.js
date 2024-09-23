

var myViewModel = {




    Modules: ko.observableArray(),
    SelectedModule: ko.observable(),
   // items: ko.observableArray([]),
    SelectedSub_Module: ko.observable(),
    Sub_Modules: ko.observableArray(),


    GetModules: function () {
        var UserId = parseInt($('#spanUserId').text());
        $.ajax({
            type: 'GET',
            url: '/api/Account/GetUserModules/' + 1,
            datatype: 'JSON',
            data: {},
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
             
                myViewModel.Modules(data);
               
                //$.each(data, function (key, value) {
                //    myViewModel.Modules.push({ Id: value.ModuleID, Name: value.ModuleName });

                //});

                //$.each(myViewModel.Modules.Sub_Modules, function (key, value) {
                //    alert(value.SubModuleID);
                //    myViewModel.Modules.Sub_Modules.push({ Id: value.SubModuleID, Name: value.SubModuleName });
                //});

            },
            error: function (data) {
                alert(data.status);
            }
        });

    },





    PTTitle: ko.observable(),
    agents: ko.observableArray([]),

   
    // Update verify status
    VerifyAgent: function (agent) {
        var AgentData = {
            ApplicantID: agent.ApplicantID,
            PortID: agent.PortID,
            WFStatus: agent.Status,

        };

        $.ajax({
            type: "PUT",
            url: "/api/AgentRegistration/PutVerifyAgent/" + agent.ApplicantID,
            data: ko.toJSON(AgentData),
            contentType: "application/json",
            dataType:'JSON',
            success: function (data) {
                if (agent.Status == "NEW")
                    alert("Record Verified Successfully");
                else
                    alert("Record Approved Successfully");

                myViewModel.agents.remove(agent);
            },
            error: function (error) {
                alert(error.status + "<!----!>" + error.statusText);
            }
        });


    },

   
    // Get Pending verify agents
    GetAgentsVerificationGrid: function (status) {
        if (status == 'N')
            myViewModel.PTTitle('Agent Registration Verification');
        else
            myViewModel.PTTitle('Agent Registration Approval');
        
        $.ajax({
            type: "GET",
            url: "/api/AgentRegistration/GetAgentRegistration",
            datatype: "JSON",
            data: {  WFStatus: status},
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                debugger;
                myViewModel.agents(data);
            },
            error: function (error) {
                debugger;
                alert(error.status + "<!----!>" + error.statusText);
            }
        });
    },
    RejectAgent: function (agent) {
        var AgentData = {
            ApplicantID: agent.ApplicantID,
            PortID: agent.PortID,
            WFStatus: agent.Status,
            RejectedRemarks: agent.RejectedRemarks,
            RejectedBy: agent.RejectedBy,
            RejectedDate: agent.RejectedDate
        };
        $.ajax({
            type: "PUT",
            url: "/api/AgentRegistration/PutRejectAgent/" + agent.ApplicantID,
            data: ko.toJSON(AgentData),
            contentType: "application/json",
            success: function (data) {
                alert("Record Rejected Successfully");
                myViewModel.agents.remove(agent);
            },
            error: function (error) {
                alert(error.status + "<!----!>" + error.statusText);
            }
        });
    },
    GotoApplicantRejectForm: function (agent) {
        var remarks = document.getElementById('remarks').value;
        agent.RejectedRemarks = remarks;
        myViewModel.RejectAgent(agent);
    },

    AgentView: function (agent) {

        window.location.href = "/AgentRegistration/ApplicantRegistration/" + agent.ApplicantID;


    },

        
   

}

ko.applyBindings(myViewModel);
myViewModel.GetModules();









