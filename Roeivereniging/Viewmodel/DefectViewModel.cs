using Model;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viewmodel {
    public static class DefectViewModel {
        private static DefectDAO DefectDB = new DefectDAO();

        public static bool AddDefect(Defect defect) {
            return DefectDB.Add(defect);
        }

        public static List<Defect> AllDefects() {
            return DefectDB.GetAll();
        }
    }
}
