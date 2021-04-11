using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationJacobian
	{
		private int rowsCount;

		private int colsCount;

		private List<float> matrixData;

		public float this[int row, int col]
		{
			get
			{
				bool flag = row < 0 || row >= this.rowsCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				bool flag2 = col < 0 || col >= this.colsCount;
				if (flag2)
				{
					throw new IndexOutOfRangeException();
				}
				return this.matrixData[row * this.colsCount + col];
			}
			set
			{
				bool flag = row < 0 || row >= this.rowsCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				bool flag2 = col < 0 || col >= this.colsCount;
				if (flag2)
				{
					throw new IndexOutOfRangeException();
				}
				this.matrixData[row * this.colsCount + col] = value;
			}
		}

		public int rows
		{
			get
			{
				return this.rowsCount;
			}
			set
			{
				this.rowsCount = value;
			}
		}

		public int columns
		{
			get
			{
				return this.colsCount;
			}
			set
			{
				this.colsCount = value;
			}
		}

		public List<float> elements
		{
			get
			{
				return this.matrixData;
			}
			set
			{
				this.matrixData = value;
			}
		}

		public ArticulationJacobian(int rows, int cols)
		{
			this.rowsCount = rows;
			this.colsCount = cols;
			this.matrixData = new List<float>(rows * cols);
			for (int i = 0; i < rows * cols; i++)
			{
				this.matrixData.Add(0f);
			}
		}
	}
}
