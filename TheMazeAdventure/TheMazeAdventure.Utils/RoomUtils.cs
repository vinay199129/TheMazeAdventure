using System;
using System.ComponentModel;
using System.Reflection;
using TheMazeAdventure.DataContracts.Enum;

namespace TheMazeAdventure.Utils
{
	public static class RoomUtils
    {
		const string Space = " ";

		static string GetDescriptionAttr<T>(this T source)
		{
			FieldInfo fi = source.GetType().GetField(source.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0].Description;
			else return source.ToString();
		}

		static TrapType GetTrapByRoomType(RoomType roomType)
		{
			TrapType trapType = TrapType.None;
			switch (roomType)
			{
				case RoomType.Desert:
					trapType = TrapType.Desert;
					break;
				case RoomType.Forest:
					trapType = TrapType.Forest;
					break;
				case RoomType.Marsh:
					trapType = TrapType.Marsh;
					break;
				case RoomType.Hills:
					trapType = TrapType.Hills;
					break;
				case RoomType.Dragon:
					trapType = TrapType.Dragon;
					break;
				default:
					trapType = TrapType.None;
					break;
			}
			return trapType;
		}

		public static string GetRoomDescription(bool causesInjury, RoomType roomType = RoomType.Entrance)
		{
			string description = roomType.GetDescriptionAttr();
			if (causesInjury)
			{
				description = description + Space + GetTrapByRoomType(roomType).GetDescriptionAttr();
			}
			return description;
		}

		public static bool CausesInjury(RoomType roomType = RoomType.Entrance)
		{
			Random rnd = new Random();
			int causesInjury = rnd.Next(1, 100);
			return causesInjury <= GetTrapTriggerPercentage(GetTrapByRoomType(roomType));
		}

		static int GetTrapTriggerPercentage(TrapType trapType = TrapType.None)
		{
			int triggerPercentage = 0;
			switch (trapType)
			{
				case TrapType.Desert:
					triggerPercentage = 15;
					break;
				case TrapType.Forest:
					triggerPercentage = 20;
					break;
				case TrapType.Marsh:
					triggerPercentage = 25;
					break;
				case TrapType.Hills:
					triggerPercentage = 30;
					break;
				case TrapType.Dragon:
					triggerPercentage = 35;
					break;
				default:
					triggerPercentage = 0;
					break;
			}
			return triggerPercentage;
		}
	}
}
