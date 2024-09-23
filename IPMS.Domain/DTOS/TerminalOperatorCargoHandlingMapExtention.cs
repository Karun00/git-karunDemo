using IPMS.Domain.Models;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class TerminalOperatorCargoHandlingMapExtention
    {
        /// <summary>
        /// Data List Transfer from TerminalOperatorCargoHandling List to Array
        /// </summary>
        /// <param name="terminalOperatorCargoHandlings"></param>
        /// <returns></returns>
        public static List<string> MapToCargokeysArray(this ICollection<TerminalOperatorCargoHandling> terminalOperatorCargoHandlings)
        {
            List<string> CargoHandlingArray = new List<string>();
            if (terminalOperatorCargoHandlings != null)
            {
             
                foreach (var terminalOperatorCargoHandling in terminalOperatorCargoHandlings)
                {
                    if (terminalOperatorCargoHandling.RecordStatus =="A")
                    {
                        CargoHandlingArray.Add(terminalOperatorCargoHandling.CargoTypeCode);
                    }
                  
                }
            }
            return CargoHandlingArray;
        }

        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        /// </summary>
        /// <param name="CargoKeys"></param>
        /// <param name="terminalOperatorId"></param>
        /// <returns></returns>
        public static List<TerminalOperatorCargoHandling> MapToEntityTerminalOperatorCargoes(this List<string> CargoKeys, int terminalOperatorId)
        {


            List<TerminalOperatorCargoHandling> terminalOperatorCargoes = new List<TerminalOperatorCargoHandling>();
            if (CargoKeys != null)
            {
                foreach (var cargoKey in CargoKeys)
                {
                    TerminalOperatorCargoHandling terminalOperatorCargo = new TerminalOperatorCargoHandling();
                    terminalOperatorCargo.TerminalOperatorID = terminalOperatorId;
                    terminalOperatorCargo.CargoTypeCode = cargoKey;

                    terminalOperatorCargoes.Add(terminalOperatorCargo);
                }
            }
            return terminalOperatorCargoes;
            //return berthKeyArray;
        }


    }
}
