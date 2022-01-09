namespace Towel_Benchmarking;

[Tag(Program.Name, "Weighted Random")]
[Tag(Program.OutputFile, nameof(WeightedRandomBenchmarks))]
public class WeightedRandomBenchmarks
{
	[Params(10, 100, 1000, 10000, 100000)]
	public int N;

	internal Random? random;
	internal int[]? array;
	internal double totalWeight;

	[IterationSetup]
	public void IterationSetup()
	{
		random = new Random(7);
		array = new int[N];
		totalWeight = 0;
		for (int i = 0; i < N; i++)
		{
			array[i] = i;
			totalWeight += i;
		}
	}

	// [Benchmark] public void Next_Stepper_TotalWeight() =>
	// random.Next<int>(step =>
	// {
	// 	foreach (int i in array)
	// 		if (step((i, i)) is Break)
	// 			return Break;
	// 	return Continue;
	// }, totalWeight);

	// [Benchmark] public void Next_Stepper() =>
	// random.Next<int>(step =>
	// {
	// 	foreach (int i in array)
	// 		if (step((i, i)) is Break)
	// 			return Break;
	// 	return Continue;
	// });

	[Benchmark]
	public void Next_IEnumerable_TotalWeight() =>
		random!.Next(array!.Select(i => (i, (double)i)), totalWeight);

	[Benchmark]
	public void Next_IEnumerable() =>
		random!.Next(array!.Select(i => (i, (double)i)));
}
