namespace Towel_Benchmarking;

[Tag(Program.Name, "Source")]
[Tag(Program.OutputFile, nameof(SourceOfBenchmarks))]
public class SourceOfBenchmarks
{
	internal bool a;
	internal bool b;

	internal Exception exception;

	public SourceOfBenchmarks()
	{
		a = false;
		b = true;
		exception = new();
	}

	[Benchmark]
	public void Control()
	{
		if (a) exception = new ArgumentException("a", "a");

		if (b) exception = new ArgumentException("b", "b");
	}

	[Benchmark]
	public void SourceOf()
	{
		if (sourceof(a, out string check1)) exception = new ArgumentException(check1, "a");

		if (sourceof(b, out string check2)) exception = new ArgumentException(check2, "b");
	}
}
