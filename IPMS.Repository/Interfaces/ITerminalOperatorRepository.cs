using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ITerminalOperatorRepository
    {
        List<TerminalOperatorVO> GetTerminalOperatorList(string portCode);
        TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO entityVo, int userId);
        TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO entityVo, int userId);
        List<SubCategoryVO> GetCargoTypes();
        List<TerminalOperatorVO> GetTerminalOperators(string portCode);
    }
}
