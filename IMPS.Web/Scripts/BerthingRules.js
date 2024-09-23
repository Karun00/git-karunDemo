window.BusinessRules = {};
(function (berthrules) {
    var BerthingRulesHelper = function () {
        var self = this;
        var isvalid;
        var isDraftValid
        var isVesselTypeValid;
        var isReasonforVisitValid;
        var isCargoTypeValid = false;
        var isTerminalOperatorBerth;
        var isBerthValid;
        self.CheckBerthingRules = function (vessel, berth, userdata, UnderKeelClearance) {

            // 1. Draft Rule
            var MaxDraft;
            var DraftRuleMsg = "";
            var CargoRuleMsg = "";
            var TORuleMsg = "";           
            MaxDraft = parseFloat(vessel.Draft()) + parseFloat(UnderKeelClearance);          



            // Is Tidal
            if (vessel.Tidal() == 'A') {                
                isDraftValid = true;
            }
            else {
                if (parseFloat(MaxDraft) <= parseFloat(berth.Draft()))
                    isDraftValid = true;
                else {
                    isDraftValid = false;
                    DraftRuleMsg = "Vessel Draft - " + parseFloat(MaxDraft) + " is more than Berth Draft - " + berth.Draft();
                }
            }


            //Cargo Type
            isCargoTypeValid = false;
            if (vessel.ArrivalReasonsString() != '' && vessel.ArrivalReasonsString() != null)
                vessel.ArrivalReasons = vessel.ArrivalReasonsString().split(',');
            if (vessel.VCN() == 'VCNDB1500064' || vessel.VCN() == 'VCNDB1500066') {               


            }

            var IsBunkering;
            var IsLayBy;

            // Added by sandeep on 29-10-2015
            var IsRepair;
            var IsStores;
            var IsDryDock;
            //-- end

            if ($.inArray('BUNK', vessel.ArrivalReasons) != -1)
                IsBunkering = true;

            if ($.inArray('LABY', vessel.ArrivalReasons) != -1)
                IsLayBy = true;

            //-- Added by sandeep on 29-10-2015
            if ($.inArray('REPA', vessel.ArrivalReasons) != -1)
                IsRepair = true;

            if ($.inArray('STOR', vessel.ArrivalReasons) != -1)
                IsStores = true;

            if ($.inArray('DRYD', vessel.ArrivalReasons) != -1)
                IsDryDock = true;

            //-- end


            //if (IsBunkering == true || IsLayBy == true) { // Commented by sandeep on 29-10-2015
            if (IsBunkering == true || IsLayBy == true || IsRepair == true || IsStores == true || IsDryDock == true) { // Added by sandeep on 29-10-2015
                isCargoTypeValid = true;
            }
            else {
                //  console.log('vessel.ArrivalReasons', vessel.ArrivalCommoditiesString() &&);
                if (vessel.ArrivalCommoditiesString() != '' && vessel.ArrivalCommoditiesString() != null)
                    vessel.ArrivalCommodities = vessel.ArrivalCommoditiesString().split(',');
                else
                    vessel.ArrivalCommodities = [];
                $.each(vessel.ArrivalCommodities, function (index, item) {
                    var isCargoValid = $.inArray(item, berth.CargoTypes());
                    if (isCargoValid != -1)
                        isCargoTypeValid = true;
                    else {
                        CargoRuleMsg = "Vessel Cargo (" + vessel.ArrivalCommoditiesNames() + ") does not match with Cargo Handled at Berth (" + berth.CargoTypeNames() + ")";
                    }
                });
            }

            // Terminal Operator 
            if (userdata.isTerminalOperator() == false) {
                isTerminalOperatorBerth = true;
            }
                // Is Terminal Operator
            else {
                isTerminalOperatorBerth = $.inArray(userdata.UserID, berth.TerminalOperators());
                if (isTerminalOperatorBerth != -1)
                    isTerminalOperatorBerth = true;
                else {
                    isTerminalOperatorBerth = false;
                    TORuleMsg = "This " + berth.BerthName() + " is not allocated for your planning."
                }
            }


            // Vessel Type
            isVesselTypeValid = $.inArray(vessel.VesselType(), berth.VesselTypes());
            isVesselTypeValid = 1;

            // Reason for Visit                  
            isReasonforVisitValid = $.inArray(vessel.ReasonforVisit(), berth.ReasonsForVisitType());

            if (isDraftValid && isTerminalOperatorBerth == true && isCargoTypeValid == true)
                isvalid = true;
            else
                isvalid = false;


            var BerthRules = { ValidBerth: isvalid, DraftValid: isDraftValid, TOBerthValid: isTerminalOperatorBerth, CargoTypeValid: isCargoTypeValid, DraftRuleMsg: DraftRuleMsg, CargoRuleMsg: CargoRuleMsg, TORuleMsg: TORuleMsg }
            return BerthRules;
        }

    }
    berthrules.BerthingRulesHelper = BerthingRulesHelper;
}(window.BusinessRules))