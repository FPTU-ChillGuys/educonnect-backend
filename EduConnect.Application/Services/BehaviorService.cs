using EduConnect.Application.DTOs.Responses.BehaviorResponses;
using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using Microsoft.EntityFrameworkCore;
using EduConnect.Domain.Entities;
using FluentValidation;
using AutoMapper;

namespace EduConnect.Application.Services
{
    public class BehaviorService : IBehaviorService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ClassBehaviorLog> _classBehaviorLogRepo;
        private readonly IGenericRepository<StudentBehaviorNote> _studentBehaviorNoteRepo;
        private readonly IValidator<CreateClassBehaviorLogRequest> _createClassBehaviorLogValidator;
        private readonly IValidator<UpdateClassBehaviorLogRequest> _updateClassBehaviorLogValidator;
        private readonly IValidator<CreateStudentBehaviorNoteRequest> _createStudentBehaviorNoteValidator;
        private readonly IValidator<UpdateStudentBehaviorNoteRequest> _updateStudentBehaviorNoteValidator;

        public BehaviorService(IMapper mapper,
                                IGenericRepository<ClassBehaviorLog> classBehaviorLogRepo,
                                IGenericRepository<StudentBehaviorNote> studentBehaviorNoteRepo,
                                IValidator<CreateClassBehaviorLogRequest> createClassBehaviorLogValidator,
                                IValidator<UpdateClassBehaviorLogRequest> updateClassBehaviorLogValidator,
                                IValidator<CreateStudentBehaviorNoteRequest> createStudentBehaviorNoteValidator,
                                IValidator<UpdateStudentBehaviorNoteRequest> updateStudentBehaviorNoteValidator)
        {
            _mapper = mapper;
            _classBehaviorLogRepo = classBehaviorLogRepo;
            _studentBehaviorNoteRepo = studentBehaviorNoteRepo;
            _createClassBehaviorLogValidator = createClassBehaviorLogValidator;
            _updateClassBehaviorLogValidator = updateClassBehaviorLogValidator;
            _updateStudentBehaviorNoteValidator = updateStudentBehaviorNoteValidator;
            _createStudentBehaviorNoteValidator = createStudentBehaviorNoteValidator;
        }

        public async Task<BaseResponse<List<ClassBehaviorLogDto>>> GetClassBehaviorLogsAsync(Guid sessionId)
        {
            var logs = await _classBehaviorLogRepo.GetAllAsync(
                filter: log => log.ClassSessionId == sessionId,
                asNoTracking: true
            );

            var dtoList = _mapper.Map<List<ClassBehaviorLogDto>>(logs);
            return BaseResponse<List<ClassBehaviorLogDto>>.Ok(dtoList);
        }

        public async Task<BaseResponse<List<StudentBehaviorNoteDto>>> GetStudentBehaviorNotesAsync(Guid sessionId)
        {
            var notes = await _studentBehaviorNoteRepo.GetAllAsync(
                filter: note => note.ClassSessionId == sessionId,
                include: q => q.Include(n => n.Student),
                asNoTracking: true
            );

            var dtoList = _mapper.Map<List<StudentBehaviorNoteDto>>(notes);
            return BaseResponse<List<StudentBehaviorNoteDto>>.Ok(dtoList);
        }

        public async Task<BaseResponse<string>> CreateClassBehaviorLogAsync(CreateClassBehaviorLogRequest request)
        {
            var validation = _createClassBehaviorLogValidator.Validate(request);
            if (!validation.IsValid)
                return BaseResponse<string>.Fail(validation.Errors.First().ErrorMessage);

            var entity = _mapper.Map<ClassBehaviorLog>(request);
            await _classBehaviorLogRepo.AddAsync(entity);
            var saved = await _classBehaviorLogRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Class behavior log created successfully")
                : BaseResponse<string>.Fail("Failed to create class behavior log");
        }

        public async Task<BaseResponse<string>> CreateStudentBehaviorNoteAsync(CreateStudentBehaviorNoteRequest request)
        {
            var validation = _createStudentBehaviorNoteValidator.Validate(request);
            if (!validation.IsValid)
                return BaseResponse<string>.Fail(validation.Errors.First().ErrorMessage);

            var entity = _mapper.Map<StudentBehaviorNote>(request);
            await _studentBehaviorNoteRepo.AddAsync(entity);
            var saved = await _studentBehaviorNoteRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Student behavior note created successfully")
                : BaseResponse<string>.Fail("Failed to create student behavior note");
        }

        public async Task<BaseResponse<string>> UpdateClassBehaviorLogAsync(Guid logId, UpdateClassBehaviorLogRequest request)
        {
            var validator = _updateClassBehaviorLogValidator;
            var result = validator.Validate(request);
            if (!result.IsValid)
                return BaseResponse<string>.Fail(result.Errors.First().ErrorMessage);

            var log = await _classBehaviorLogRepo.GetByIdAsync(l => l.LogId == logId);
            if (log == null)
                return BaseResponse<string>.Fail("Log not found");

            log.BehaviorType = request.BehaviorType;
            log.Note = request.Note;

            _classBehaviorLogRepo.Update(log);
            var saved = await _classBehaviorLogRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Class behavior log updated")
                : BaseResponse<string>.Fail("Failed to update log");
        }

        public async Task<BaseResponse<string>> UpdateStudentBehaviorNoteAsync(Guid noteId, UpdateStudentBehaviorNoteRequest request)
        {
            var validator = _updateStudentBehaviorNoteValidator;
            var result = validator.Validate(request);
            if (!result.IsValid)
                return BaseResponse<string>.Fail(result.Errors.First().ErrorMessage);

            var note = await _studentBehaviorNoteRepo.GetByIdAsync(n => n.NoteId == noteId);
            if (note == null)
                return BaseResponse<string>.Fail("Note not found");

            note.BehaviorType = request.BehaviorType;
            note.Comment = request.Comment;

            _studentBehaviorNoteRepo.Update(note);
            var saved = await _studentBehaviorNoteRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Student behavior note updated")
                : BaseResponse<string>.Fail("Failed to update note");
        }

        public async Task<BaseResponse<string>> DeleteClassBehaviorLogAsync(Guid logId)
        {
            var log = await _classBehaviorLogRepo.GetByIdAsync(l => l.LogId == logId);
            if (log == null)
                return BaseResponse<string>.Fail("Log not found");

            _classBehaviorLogRepo.Remove(log);
            var saved = await _classBehaviorLogRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Class behavior log deleted")
                : BaseResponse<string>.Fail("Failed to delete log");
        }

        public async Task<BaseResponse<string>> DeleteStudentBehaviorNoteAsync(Guid noteId)
        {
            var note = await _studentBehaviorNoteRepo.GetByIdAsync(n => n.NoteId == noteId);
            if (note == null)
                return BaseResponse<string>.Fail("Note not found");

            _studentBehaviorNoteRepo.Remove(note);
            var saved = await _studentBehaviorNoteRepo.SaveChangesAsync();

            return saved
                ? BaseResponse<string>.Ok("Student behavior note deleted")
                : BaseResponse<string>.Fail("Failed to delete note");
        }

        public async Task<BaseResponse<List<ClassBehaviorLogDto>>> GetClassBehaviorLogsBySearchAsync(string? search)
        {
           

            var classBehaviorLogs = await _classBehaviorLogRepo.GetAllAsync(
                log => log.ClassSession!.Class.ClassName.Contains(search),
                include: q => q.Include(l => l.ClassSession).ThenInclude(cs => cs.Class),
                asNoTracking: true
                );

            return BaseResponse<List<ClassBehaviorLogDto>>.Ok(
                _mapper.Map<List<ClassBehaviorLogDto>>(classBehaviorLogs));
        }

        public async Task<BaseResponse<List<StudentBehaviorNoteDto>>> GetStudentBehaviorNotesBySearchAsync(string? search)
        {
            var studentBehaviorNotes = await _studentBehaviorNoteRepo.GetByIdAsync(
                note => note!.Student!.FullName.Contains(search) || String.IsNullOrEmpty(search),
                include: q => q.Include(n => n.Student),
                asNoTracking: true
            );

            return BaseResponse<List<StudentBehaviorNoteDto>>.Ok(
                _mapper.Map<List<StudentBehaviorNoteDto>>(studentBehaviorNotes));
        }
    }
}
