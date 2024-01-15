
using BusinessObject;
using DataAccess;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IOrderRepository _orderRepository;
        public MemberRepository(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private Member MapToMember(MemberObject memberObject)
        {
            return new Member
            {
                MemberId = memberObject.MemberId,
                City = memberObject.City,
                CompanyName = memberObject.CompanyName,
                Country = memberObject.Country,
                Password = memberObject.Password,
                Email = memberObject.Email
            };
        }

        private MemberObject? MapToMemberObject(Member? member)
        {
            return member == null ? null : new MemberObject
            {
                MemberId = member.MemberId,
                City = member.City,
                CompanyName = member.CompanyName,
                Country = member.Country,
                Password = member.Password,
                Email = member.Email
            };
        }


        public void AddMember(MemberObject member)
        {
            Member _member = MapToMember(member);
            MemberDAO.Instance.AddMember(_member);
        }

        public void DeleteMember(int id)
        {
            _orderRepository.DeleteOrderByMemberId(id);
            MemberDAO.Instance.DeleteMember(id);
        }

        public MemberObject? GetMember(int id)
        {
            Member? member = MemberDAO.Instance.GetMember(id);
            return MapToMemberObject(member);
        }

        public List<MemberObject> GetMembers()
        {
            List<Member> members = MemberDAO.Instance.GetMembers();
            return members.ConvertAll<MemberObject>(member => MapToMemberObject(member));
        }

        public void UpdateMember(MemberObject member)
        {
            Member _member = MapToMember(member);
            MemberDAO.Instance.UpdateMember(_member);
        }

        public MemberObject? Login(string email, string password)
        {
            Member? member = MemberDAO.Instance.GetMemberByEmailAndPassword(email, password);
            return MapToMemberObject(member);
        }
    }
}
