using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viewmodel {
    public static class RepairsViewModel {
        private static RepairsDAO RepairsDB = new RepairsDAO();

        public static bool AddRepair(Defect defect, string repairNote, Member repairer) {
            return RepairsDB.AddRepair(defect, repairNote, repairer);
        }
    }
}
