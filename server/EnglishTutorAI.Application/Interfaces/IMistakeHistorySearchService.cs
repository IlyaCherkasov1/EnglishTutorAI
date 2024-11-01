﻿using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;

namespace EnglishTutorAI.Application.Interfaces;

public interface IMistakeHistorySearchService
{
    Task<IEnumerable<MistakeHistoryItems>> Search(PaginationSearchModel model);
}