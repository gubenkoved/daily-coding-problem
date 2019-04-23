<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// We're given a hashmap associating each courseId key
// with a list of courseIds values, which represents that
// the prerequisites of courseId are courseIds. Return a
// sorted ordering of courses such that we can finish all
// courses.
// 
// Return null if there is no such ordering.
// 
// For example, given {'CSC300': ['CSC100', 'CSC200'],
// 'CSC200': ['CSC100'], 'CSC100': []}, should return
// ['CSC100', 'CSC200', 'CSCS300'].

void Main()
{
	CreateOrdering(new[]
	{
		new Course("CSC300", new string[] { "CSC100", "CSC200" }),
		new Course("CSC200", new string[] { "CSC100", }),
		new Course("CSC100", new string[] { }),
	}).Select(x => x.Id).Dump("CSC100 > CSC200 > CSC300");

	CreateOrdering(new[]
	{
		new Course("5", new string[] { "4", "3" }),
		new Course("4", new string[] { "3" }),
		new Course("3", new string[] { "2", "1" }),
		new Course("2", new string[] { "1" }),
		new Course("1", new string[] { }),
	}).Select(x => x.Id).Dump("1 > 2 > 3 > 4 > 5");

	CreateOrdering(new[]
	{
		new Course("a", new string[] { "b" }),
		new Course("b", new string[] { "a" }),
	}).Dump("null");
}

class Course
{
	public string Id { get; set; }
	public IEnumerable<string> DependsOn { get; set; }

	public Course(string id, IEnumerable<string> dependsOn)
	{
		Id = id;
		DependsOn = dependsOn ?? new string[] { };
	}
}

IEnumerable<Course> CreateOrdering(IEnumerable<Course> courses)
{
	Dictionary<string, Course> map = courses.ToDictionary(x => x.Id, x => x);
	List<Course> toArrange = courses.ToList();
	List<Course> ordered = new List<Course>();
	HashSet<Course> resolved = new HashSet<Course>();
	
	Course next;
	
	do
	{
		next = null;
		
		foreach (var course in toArrange)
		{
			bool allResolved = !course.DependsOn.Any() || course.DependsOn.All(id => resolved.Contains(map[id]));

			if (allResolved)
			{
				next = course;
				break;
			}
		}
		
		if (next != null)
		{
			ordered.Add(next);
			resolved.Add(next);
			toArrange.Remove(next);
		}
		
	} while (next != null);
	
	if (toArrange.Any())
		return null;
		
	return ordered;
}
