using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<MemberObject> GetMembers();

        MemberObject GetMember(int id);

        void AddMember(MemberObject member);
        void DeleteMember(int id);
        void UpdateMember(MemberObject member);
        MemberObject Login(string email, string password);
    }
}
