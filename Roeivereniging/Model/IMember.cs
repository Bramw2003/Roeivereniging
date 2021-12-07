using System;
using System.Collections.Generic;
using System.Text;

namespace Model {
    interface IMember {
        bool insert(Member member);

        bool Update(Member member);

        bool Delete(int memberID);
    }
}
