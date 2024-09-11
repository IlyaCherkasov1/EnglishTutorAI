using EnglishTutorAI.Domain.Enums;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Models;

public record GenerateLastMessageModel(RunResponse Run, string ThreadId, ChatType ChatType);