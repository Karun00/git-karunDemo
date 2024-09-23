$(function () {
    var agentViewModel = function () {
        var self = this;
        self.Agents = ko.observableArray([]);
        self.EditAgent = function (agent) {
            EditAgent(agent);
        },
        self.ViewAgent = function (agent) {
            ViewAgent(agent);
        }
    }
    var vm = new agentViewModel();
    ko.applyBindings(vm);
    $.ajax({
        url: 'api/AgentRegistration/GetAllApplicants',
        type: 'get',
        dtatType: 'json',
        success: function (data) {
            vm.Agents(data);
        },
        error: function (data) { alert(data.status) }
    });
    
});


function EditAgent(agent) {
    window.location = ROOT + 'AgentRegistration/Registration/' + agent.ApplicantID;
}
function ViewAgent(agent) {

}