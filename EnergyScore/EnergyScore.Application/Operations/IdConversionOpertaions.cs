using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Application.Operations
{
    public interface IIdConversionOpertaions
    {
        public string GuidToHPXMLIDConvertor(Guid id);
        public Guid HPXMLIDToGuidConvertor(String id);
    }
    public class IdConversionOpertaions : IIdConversionOpertaions
    {
        string IdStartsWith = "HPXMLId-";
        public string GuidToHPXMLIDConvertor(Guid id)
        {
            return IdStartsWith + id.ToString();
        }   
        public Guid HPXMLIDToGuidConvertor(String id)
        {
            return Guid.Parse(id.Replace(IdStartsWith, ""));
        }
    }
}
