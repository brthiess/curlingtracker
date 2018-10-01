using System;
using CurlingTracker.Models;
using System.Collections.Generic;

public interface IFeedParser
{
	List<News> GetNewsFeed();
}
