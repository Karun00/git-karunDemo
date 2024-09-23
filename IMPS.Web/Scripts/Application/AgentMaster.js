var agentVM = {
    AgentData: ko.observableArray([]),
    GetAll: function () {
        //var self = this;
        
        $.ajax(
            
 {
     
     url: "/api/AgentRegistration/GetAgents",
     async: false,
     dataType: 'json',
     success: function (data) {
         alert(ko.toJSON(data));
         agentVM.AgentData(data);
     }
 });
    }
};
ko.applyBindings(agentVM);
agentVM.GetAll();