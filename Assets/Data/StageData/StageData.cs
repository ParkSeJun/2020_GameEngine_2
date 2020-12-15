using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = 1000)]
public class StageData : ScriptableObject
{
	[Serializable]
	public struct MonsterData
	{
		public Rect region;
		public float y;
		public int count;
	}


	[SerializeField] int stageId;
	[SerializeField] float monsterStatMultiplier;
	[SerializeField] MonsterData[] monsters;

	public int StageId => stageId;
	public float MonsterStatMultiplier => monsterStatMultiplier;
	public MonsterData[] Monsters => monsters;

}
