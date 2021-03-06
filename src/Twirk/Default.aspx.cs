﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WiRK.Abacus;
using WiRK.Terminator;

namespace WiRK.TwirkIt
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ViewState["PosX"] = "0";
			ViewState["PosY"] = "0";
			ViewState["Facing"] = "0";
			ViewState["Cards"] = string.Empty;
		}

		protected void btnRunSimulations_OnClick(object sender, EventArgs e)
		{
			List<int> position = Request.Form["robotPosition"].Split(',').Select(int.Parse).ToList();
			var cards = Request.Form["cards"].Split(',').Select(int.Parse);

			var robot = new Robot 
			{
				Position = new Coordinate {X = position[0], Y = position[1]},
				Facing = (Orientation)Enum.Parse(typeof(Orientation), Request.Form["robotOrientation"])
			};
			foreach (var c in cards)
			{
				robot.DealCard(c);
			}

			var game = new Game { Board = { Squares = Maps.GetMap(Maps.MapLayouts.ScottRallyMap) }, Robots = new List<Robot> { robot } };
			game.Initialize();

			List<List<CardExecutionResult>> results = Simulator.RunSimulations(robot);
			List<List<CardExecutionResult>> productiveResults = results.Where(result => result.Last().Position.X != -1).ToList();

			ViewState["PosX"] = position[0];
			ViewState["PosY"] = position[1];
			ViewState["Facing"] = Request.Form["robotOrientation"];
			ViewState["Cards"] = Request.Form["cards"];

			ClientScript.RegisterClientScriptBlock(GetType(), "results", "results = " + JsonConvert.SerializeObject(productiveResults, Formatting.Indented), true);
		}
	}
}