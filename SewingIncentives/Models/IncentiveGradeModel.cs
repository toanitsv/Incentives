using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SewingIncentives.Models
{
    public class IncentiveGradeModel
    {
        public static List<IncentiveGradeModel> Select()
        {
            List<IncentiveGradeModel> incentiveGradeList = new List<IncentiveGradeModel>();
            //Sewing
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "NaN",
                Ratio = 0,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "a",
                Ratio = 1,
                IsSmallGrade = true,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "A",
                Ratio = 1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "B",
                Ratio = 1.1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "C",
                Ratio = 1.19,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "D/LL 2",
                Ratio = 1.27,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "LL 1",
                Ratio = 1.2,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "LL 3",
                Ratio = 1.34,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "SUP 4",
                Ratio = 1.4,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "E",
                Name = "SUP 5",
                Ratio = 1.47,
                IsSmallGrade = false,
            });
            //Assembly
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "NaN",
                Ratio = 0,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "Worker",
                Ratio = 1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "Draw Line/Monitor",
                Ratio = 1.1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "Mech 1/Over Seaming/Side, Hell & Slip Last/Press Machine/Outsole Stitching/Powe/Buffing/Injection",
                Ratio = 1.2,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "LL 1/Toe, Lasting/Outsole Attach Cementing/Secrectary",
                Ratio = 1.25,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "LL 2/QC 2/Mech 2",
                Ratio = 1.35,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "LL 3/QC 3/Mech 3",
                Ratio = 1.5,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "SUP 4",
                Ratio = 1.7,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "SUP 5",
                Ratio = 1.85,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "F",
                Name = "SUP 6",
                Ratio = 2.0,
                IsSmallGrade = false,
            });
            //Outsole
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "G",
                Name = "NaN",
                Ratio = 0,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "G",
                Name = "A",
                Ratio = 1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "G",
                Name = "B",
                Ratio = 1.1,
                IsSmallGrade = false,
            });
            incentiveGradeList.Add(new IncentiveGradeModel
            {
                SectionId = "G",
                Name = "C",
                Ratio = 1.15,
                IsSmallGrade = false,
            });

            return incentiveGradeList;
        }

        public string SectionId { get; set; }
        public string Name { get; set; }
        public double Ratio { get; set; }
        public bool IsSmallGrade { get; set; }
    }
}
