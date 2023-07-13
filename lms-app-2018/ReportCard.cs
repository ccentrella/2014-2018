using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public class ReportCard
	{
		/// <summary>
		/// Initializes a new instance of ReportCard
		/// </summary>
		/// <param name="fileLocation">The location of the XML file containing the information for the current grade level</param>
		public ReportCard(string fileLocation)
		{
			#region Variables
			int totalExamGrade = 0;
			int totalHomeworkGrade = 0;
			int totalQuizGrade = 0;
			TimeSpan totalTime = new TimeSpan();
			var days = new List<DateTime>();

			// Get assignments
			var assignments = Assignment.LoadAssignmentFile(fileLocation);
			var gradedAssignments = from assign in assignments
									where assign.Grade.HasValue
									where assign.AssignmentType == AssignmentType.Homework
									select assign;
			var gradedQuizzes = from assign in assignments
								where assign.Grade.HasValue
								where assign.AssignmentType == AssignmentType.Quiz
								select assign;
			var gradedExams = from assign in assignments
							  where assign.Grade.HasValue
							  where assign.AssignmentType == AssignmentType.Exam
							  select assign;
			var timedAssignments = from assign in assignments
								   where assign.Time.HasValue
								   select assign;
			#endregion

			// Get values
			foreach (var assign in gradedAssignments)
            {
                totalHomeworkGrade += assign.Grade.Value;
            }

            foreach (var assign in gradedQuizzes)
            {
                totalQuizGrade += assign.Grade.Value;
            }

            foreach (var assign in gradedExams)
            {
                totalExamGrade += assign.Grade.Value;
            }

            foreach (var assign in timedAssignments)
			{
				totalTime += assign.Time.Value;
				foreach (var date in assign.Date)
                {
                    if (!days.Contains(date))
                    {
                        days.Add(date);
                    }
                }
            }

			// Update properties
			GradeLevel = Path.GetFileNameWithoutExtension(fileLocation);
			int examCount = gradedExams.Count();
			int quizCount = gradedQuizzes.Count();
			int assignmentCount = gradedAssignments.Count();
			int timeCount = timedAssignments.Count();
			if (examCount > 0)
            {
                AverageTestGrade = (byte)Math.Round((double)totalExamGrade / examCount);
            }

            if (quizCount > 0)
            {
                AverageQuizGrade = (byte)Math.Round((double)totalQuizGrade / quizCount);
            }

            if (assignmentCount > 0)
            {
                AverageHomeworkGrade = (byte)Math.Round((double)totalHomeworkGrade / assignmentCount);
            }

            if (timeCount > 0)
			{
				var dailyTime = (int)Math.Round(totalTime.TotalSeconds / timeCount);
				AverageAssignmentTime = new TimeSpan(0, 0, dailyTime);
				TotalTime = totalTime;
				var assignmentTime = (int)Math.Round(totalTime.TotalSeconds / days.Count());
				AverageTime = new TimeSpan(0, 0, assignmentTime);
			}
		}

		/// <summary>
		/// The user's grade level which this report card represents
		/// </summary>
		public string GradeLevel { get; set; }

		/// <summary>
		/// The user's average test grade for the specified grade level
		/// </summary>
		public byte? AverageTestGrade { get; set; }

		/// <summary>
		/// The user's average homework grade for the specified grade level
		/// </summary>
		public byte? AverageHomeworkGrade { get; set; }

		/// <summary>
		/// The user's average quiz grade for the specified grade level
		/// </summary>
		public byte? AverageQuizGrade { get; set; }

		/// <summary>
		/// The user's average daily time for the current grade level
		/// </summary>
		public TimeSpan? AverageTime { get; set; }

		/// <summary>
		/// The user's average assignment time for the current grade level
		/// </summary>
		public TimeSpan? AverageAssignmentTime { get; set; }

		/// <summary>
		/// The total time the user has spent on the current grade level
		/// </summary>
		public TimeSpan? TotalTime { get; set; }
	}
}
