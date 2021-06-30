namespace Towel.Source.Mathematics
{
#if false

	//public class Tableau
	//{
	//const generic epsilon = 1.0e-8;
	////int equal(generic a, generic b) { return fabs(a - b) < epsilon; }

	//static void pivot_on(ref generic[,] tableau, int row, int col)
	//{
	//	int i, j;
	//	generic pivot;

	//	pivot = tableau[row, col];
	//	if (!(pivot > 0))
	//		throw new System.Exception("possible invalid tableau values (IDK)");
	//	for (j = 0; j < tableau.GetLength(1); j++)
	//		tableau[row, j] /= pivot;
	//	if (!(Logic.Equate(tableau[row, col], 1, epsilon)))
	//		throw new System.Exception("possible invalid tableau values (IDK)");

	//	for (i = 0; i < tableau.GetLength(0); i++)
	//	{ // foreach remaining row i do
	//		generic multiplier = tableau[i, col];
	//		if (i == row) continue;
	//		for (j = 0; j < tableau.GetLength(1); j++)
	//		{ // r[i] = r[i] - z * r[row];
	//			tableau[i, j] -= multiplier * tableau[row, j];
	//		}
	//	}
	//}

	//// Find pivot_col = most negative column in mat[0][1..n]
	//static int find_pivot_column(ref generic[,] tableau)
	//{
	//	int j, pivot_col = 1;
	//	generic lowest = tableau[0, pivot_col];
	//	for (j = 1; j < tableau.GetLength(1); j++)
	//	{
	//		if (tableau[0, j] < lowest)
	//		{
	//			lowest = tableau[0, j];
	//			pivot_col = j;
	//		}
	//	}
	//	//printf("Most negative column in row[0] is col %d = %g.\n", pivot_col, lowest);
	//	if (lowest >= 0)
	//	{
	//		return -1; // All positive columns in row[0], this is optimal.
	//	}
	//	return pivot_col;
	//}

	//// Find the pivot_row, with smallest positive ratio = col[0] / col[pivot]
	//static int find_pivot_row(ref generic[,] tableau, int pivot_col)
	//{
	//	int i, pivot_row = 0;
	//	generic min_ratio = -1;
	//	//printf("Ratios A[row_i,0]/A[row_i,%d] = [", pivot_col);
	//	for (i = 1; i < tableau.GetLength(0); i++)
	//	{
	//		generic ratio = tableau[i, 0] / tableau[i, pivot_col];
	//		//printf("%3.2lf, ", ratio);
	//		if ((ratio > 0 && ratio < min_ratio) || min_ratio < 0)
	//		{
	//			min_ratio = ratio;
	//			pivot_row = i;
	//		}
	//	}
	//	//printf("].\n");
	//	if (min_ratio == -1)
	//		return -1; // Unbounded.
	//	//printf("Found pivot A[%d,%d], min positive ratio=%g in row=%d.\n",
	//	//	pivot_row, pivot_col, min_ratio, pivot_row);
	//	return pivot_row;
	//}

	//static void add_slack_variables(ref generic[,] tableau)
	//{

	//	generic[,] newTableau =
	//		new generic[tableau.GetLength(0), tableau.GetLength(1) + tableau.GetLength(0) - 1];

	//	for (int a = 0, a_max = tableau.GetLength(0); a < a_max; a++)
	//		for (int b = 0, b_max = tableau.GetLength(1); b < b_max; b++)
	//			newTableau[a, b] = tableau[a, b];

	//	int i, j;
	//	for (i = 1; i < tableau.GetLength(0); i++)
	//	{
	//		for (j = 1; j < tableau.GetLength(0); j++)
	//			newTableau[i, j + tableau.GetLength(1) - 1] = (i == j ? 1d : 0d);
	//	}

	//	tableau = newTableau;
	//}

	//static void check_b_positive(ref generic[,] tableau)
	//{
	//	int i;
	//	for (i = 1; i < tableau.GetLength(0); i++)
	//		if (!(tableau[i, 0] >= 0))
	//			throw new System.Exception("possible invalid tableau values (IDK)");
	//}

	//// Given a column of identity matrix, find the row containing 1.
	//// return -1, if the column as not from an identity matrix.
	//static int find_basis_variable(ref generic[,] tableau, int col)
	//{
	//	int i, xi = -1;
	//	for (i = 1; i < tableau.GetLength(0); i++)
	//	{
	//		if (Logic.Equate(tableau[i, col], 1, epsilon))
	//			if (xi == -1)
	//				xi = i;	 // found first '1', save this row number.
	//			else
	//				return -1; // found second '1', not an identity matrix.
	//		else if (!Logic.Equate(tableau[i, col], 0, epsilon))
	//			return -1; // not an identity matrix column.
	//	}
	//	return xi;
	//}

	//static generic[] print_optimal_vector(ref generic[,] tableau)
	//{
	//	generic[] vector = new generic[tableau.GetLength(1)];
	//	int j, xi;
	//	//printf("%s at ", message);
	//	for (j = 1; j < tableau.GetLength(1); j++)
	//	{ // for each column.
	//		xi = find_basis_variable(ref tableau, j);
	//		if (xi != -1)
	//			vector[j] = tableau[xi, 0];
	//		else
	//			vector[j] = j;
	//	}
	//	return vector;
	//}

	//public static generic[] Simplex(ref generic[,] tableau)
	//{
	//	int loop = 0;
	//	add_slack_variables(ref tableau);
	//	check_b_positive(ref tableau);
	//	while (++loop > 0)
	//	{
	//		int pivot_col, pivot_row;

	//		pivot_col = find_pivot_column(ref tableau);
	//		if (pivot_col < 0)
	//			//printf("Found optimal value=A[0,0]=%3.2lf (no negatives in row 0).\n",
	//			//	tableau->mat[0][0]);
	//			return print_optimal_vector(ref tableau);
	//		//printf("Entering variable x%d to be made basic, so pivot_col=%d.\n",
	//		//	pivot_col, pivot_col);

	//		pivot_row = find_pivot_row(ref tableau, pivot_col);
	//		if (pivot_row < 0)
	//			throw new System.Exception("unbounded (no pivot_row)");
	//		//printf("Leaving variable x%d, so pivot_row=%d\n", pivot_row, pivot_row);

	//		pivot_on(ref tableau, pivot_row, pivot_col);
	//		//print_tableau(tableau, "After pivoting");
	//		//return print_optimal_vector(ref tableau);
	//	}
	//	throw new System.Exception("Simplex has a glitch");
	//}
	//}

#endif
}
