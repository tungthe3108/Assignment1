using DataAccess;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        public static MemberDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberDAO();
                }
                return instance;
            }
        }
        private MemberDAO() { }

        public List<Member> GetMembers()
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Members.ToList();
        }

        public Member? GetMember(int id)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Members.SingleOrDefault(member => member.MemberId == id);
        }

        private bool GetMemberByEmail(string email)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Members.Any(member => member.Email == email);
        }

        public void AddMember(Member member)
        {
            if(GetMemberByEmail(member.Email))
            {
                throw new Exception("Member is already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Members.Add(member);
            dbProvider.SaveChanges();
        }


        public void DeleteMember(int id)
        {
            Member? member = GetMember(id);
            if(member == null)
            {
                throw new Exception("This member does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Members.Remove(member);
            dbProvider.SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            Member? _member = GetMember(member.MemberId);
            if (_member == null)
            {
                throw new Exception("This member does not already exist.");
            }
            var dbProvider = new EStoreContext();
            dbProvider.Entry<Member>(member).State = EntityState.Modified;
            dbProvider.SaveChanges();
        }

        public Member? GetMemberByEmailAndPassword(String email, String password)
        {
            var dbProvider = new EStoreContext();
            return dbProvider.Members.FirstOrDefault(member => member.Password == password && member.Email == email);  
        }
    }
}
