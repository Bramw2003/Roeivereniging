using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    public interface IMember {
        bool insert(Member member);
        bool Delete(int memberID);
        Member GetByUsername(string username);
        Member GetById(int id);
    }
}
