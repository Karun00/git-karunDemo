


var AgentViewModel = function () {
    //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI 
    self.ApplicantID = ko.observable("0");
    self.PortID = ko.observable("");
    self.WFStatus = ko.observable("");
 

     
    var AgentData = {
        ApplicantID: 10,       
        PortID: 1,
        WFStatus: "N",     
    };


    //Declare an ObservableArray for Storing the JSON Response
    self.Agents = ko.observableArray([]);


    GetAgents(); //Call the Function which gets all records using ajax call



    //Function to perform Verify Operation
    self.verify = function () {     
        $.ajax({
            type: "PUT",
            url: "/api/AgentRegistration/" + 10,
            data: ko.toJSON(AgentData),
            contentType: "application/json",
            success: function (data) {
                alert("Record Verified Successfully");
                GetAgents();
            },
            error: function (error) {
                alert(error.status + "<!----!>" + error.statusText);
            }
        });
    };

    

    //Function to Read All Employees
    function GetAgents() {
        //Ajax Call Get All Employee Records
        $.ajax({
            type: "GET",
            url: "/api/AgentRegistration",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Agents; //Put the response in ObservableArray
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
        //Ends Here
    }


    //Function to Display record to be updated
    //self.getselectedagent = function (agent) {
    //    self.EmpNo(employee.EmpNo),
    //    self.EmpName(employee.EmpName),
    //    self.Salary(employee.Salary),
    //    self.DeptName(employee.DeptName),
    //    self.Designation(employee.Designation)
    //};

 
};
ko.applyBindings(new AgentViewModel());