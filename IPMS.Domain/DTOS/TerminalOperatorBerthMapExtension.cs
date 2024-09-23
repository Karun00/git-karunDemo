using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class TerminalOperatorBerthMapExtension
    {
        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorBerth List
        /// </summary>
        /// <param name="terminalOperatorBerths"></param>
        /// <returns></returns>
        public static List<string> MapToBerthKeysArray(this ICollection<TerminalOperatorBerth> terminalOperatorBerths)
        {
            List<string> berthKeyArray = new List<string>();
            if (terminalOperatorBerths != null)
            {

                foreach (var terminalOperatorBerth in terminalOperatorBerths)
                {
                    if (terminalOperatorBerth.RecordStatus == "A")
                    {
                        berthKeyArray.Add(terminalOperatorBerth.PortCode + "." + terminalOperatorBerth.QuayCode + "." + terminalOperatorBerth.BerthCode);
                    }
                }
            }
            return berthKeyArray;
        }


        //public static List<BerthVO> MapToBerthKeysArrayOnTOBaseBerthCombo(this List<TerminalOperatorBerth> terminalOperatorBerths)
        //{
        //    List<BerthVO> terminalOperatorBerthsVOs = new List<BerthVO>();
        //    foreach (var terminalOperatorBerth in terminalOperatorBerths)
        //    {
        //        if (terminalOperatorBerth.RecordStatus == "A")
        //        {
        //            terminalOperatorBerthsVOs.Add(terminalOperatorBerth.MapToBerthKeysArrayOnTOBaseBerthCombo());    
        //        }
        //    }
        //    return terminalOperatorBerthsVOs;
        //}

        //public static BerthVO MapToBerthKeysArrayOnTOBaseBerthCombo(this TerminalOperatorBerth data)
        //{
        //    BerthVO terminalOperatorBerthsVO = new BerthVO();
        //    terminalOperatorBerthsVO.BerthKey = data.Berth.PortCode + "." + data.Berth.QuayCode + "." + data.Berth.BerthCode;
        //    terminalOperatorBerthsVO.BerthName = data.Berth.BerthName;
        //    return terminalOperatorBerthsVO;
        //}
    
    
         

        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorBerth List
        /// </summary>
        /// <param name="berthKeys"></param>
        /// <param name="terminalOperatorId"></param>
        /// <returns></returns>
        public static List<TerminalOperatorBerth> MapToEntityTerminalOperatorBerths(this List<string> berthKeys, int terminalOperatorId)
        {


            List<TerminalOperatorBerth> terminalOperatorBerths = new List<TerminalOperatorBerth>();
            if (berthKeys != null)
            {
                foreach (var berthKey in berthKeys)
                {
                    string[] fields = berthKey.Split('.');
                    string portCode = fields[0];
                    string quayCode = fields[1];
                    string berthCode = fields[2];

                    TerminalOperatorBerth terminalOperatorBerth = new TerminalOperatorBerth();
                    terminalOperatorBerth.TerminalOperatorID = terminalOperatorId;
                    terminalOperatorBerth.PortCode = portCode;
                    terminalOperatorBerth.QuayCode = quayCode;
                    terminalOperatorBerth.BerthCode = berthCode;

                    terminalOperatorBerths.Add(terminalOperatorBerth);
                }
            }
            return terminalOperatorBerths;
        }




        public static TerminalOperatorVO BirthTOMapToDTO(this TerminalOperator data)
        {
            TerminalOperatorVO terminaloperatorVO = new TerminalOperatorVO();
            terminaloperatorVO.TerminalOperatorID = data.TerminalOperatorID;
            terminaloperatorVO.RegisteredName = data.RegisteredName;
            return terminaloperatorVO;
        }


    }
}
