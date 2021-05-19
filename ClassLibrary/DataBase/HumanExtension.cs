using System;
using System.IO;
using System.Text;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase
{
	static class HumanDataBaseExtension
	{
		public static void AddHumanHTMLPage(this Human human, string wayFolder)
		{
			string patternHTML = 
				@"<!doctype html>
					<html>
						<head>

						<style>
						table {
							width: 100 %;
							margin - bottom: 20px;
							border: 5px solid #fff;
							border - top: 5px solid #fff;
							border - bottom: 3px solid #fff;
							border - collapse: collapse;
							outline: 3px solid #ffd300;
							font - size: 15px;
							background: #fff!important;
						}
						.table th {
							font - weight: bold;
							padding: 7px;
							background: #ffd300;
							border: none;
							text - align: left;
							font - size: 15px;
							border - top: 3px solid #fff;
							border - bottom: 3px solid #ffd300;
						}
						.table td {
							padding: 7px;
							border: none;
							border - top: 3px solid #fff;
							border - bottom: 3px solid #fff;
							font - size: 15px;
						}
						.table tbody tr: nth - child(even){
							background: #f8f8f8!important;
						}

						</style>
					</head>
					<body>

						<table class='table'>
							<thead>
								<tr>
									<th>ФИО</th>
									<th>Дата рождения</th>
									<th>Место рождения</th>
									<th>Номер Паспорта</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>" + human.NameSurnamePatronymic.ToString() + @"</td>
									<td>" + human.DateBirth.ToString() + @"</td>
									<td>" + human.PlaceBirth + @"</td>
									<td>" + human.Passport + @"</td>
								</tr>
						</body>
					</html>";

			if (File.Exists(Convert.ToString(wayFolder + human.GetHashCode()) + ".html"))
			{
				return;
			}

			using (FileStream file = new(Convert.ToString(wayFolder) + human.GetHashCode() + ".html", FileMode.Create))
			{
				byte[] contentFile = new UTF8Encoding(true).GetBytes(patternHTML);
				file.Write(contentFile, 0, contentFile.Length);
			}
		}
	}
}
