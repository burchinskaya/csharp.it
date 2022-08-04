﻿using System;
using System.Net;
using AutoMapper;
using csharp_it.Dto;
using csharp_it.Models;
using csharp_it.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csharp_it.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _service;
        private readonly IAccountService _account;
        private readonly IQuestionService _questions;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerService service, IMapper mapper,
            IAccountService account, IQuestionService questions)
        {
            _service = service;
            _mapper = mapper;
            _account = account;
            _questions = questions;
        }

        [HttpGet("ReadById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var answer = await _service.GetAnswerByIdAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            var courseId = answer.Question.Lesson.Chapter.CourseId;

            if (await _account.CheckAccessToCourse(courseId, "SEE_ANSWERS_AND_CHECK_TEST"))
            {
                return Ok(_mapper.Map<AnswerDto>(answer));
            }
            else
            {   
                return Forbid();
            }
        }

        [HttpGet("ReadByQuestionId/{questionId}")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetByLesson(int questionId)
        {
            var answers = await _service.GetAnswersByQuestionIdAsync(questionId);

            if (answers == null)
            {
                return NotFound();
            }

            var courseId = answers.First().Question.Lesson.Chapter.CourseId;

            if (await _account.CheckAccessToCourse(courseId, "SEE_ANSWERS_AND_CHECK_TEST"))
            {
                return Ok(_mapper.Map<IEnumerable<AnswerDto>>(answers));
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAnswer(AnswerDto answer)
        {
            var user = await _account.GetCurrentUserAsync();
            var question = await _questions.GetQuestionByIdAsync(answer.QuestionId);
            var course = question.Lesson.Chapter.Course;
            if (user.Id != course.AuthorId)
            {
                return Forbid();
            }

            var _answer = _mapper.Map<Answer>(answer);
            return Created("Answer was created successfully", await _service.CreateAnswerAsync(_answer));
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateAnswer(AnswerDto answer)
        {
            var user = await _account.GetCurrentUserAsync();
            var question = await _questions.GetQuestionByIdAsync(answer.QuestionId);
            if (user.Id != question.Lesson.Chapter.Course.AuthorId)
            {
                return Forbid();
            }

            var _answer = _mapper.Map<Answer>(answer);
            return StatusCode((int)HttpStatusCode.NoContent, await _service.UpdateAnswerAsync(_answer));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _account.GetCurrentUserAsync();
            var answer = await _service.GetAnswerByIdAsync(id);

            if (answer == null)
            {
                BadRequest();
            }

            if (user.Id != answer.Question.Lesson.Chapter.Course.AuthorId)
            {
                return Forbid();
            }

            await _service.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPost]
        public async Task<double> CheckTest(List<int> answers)
        {
            double mark = await _service.CheckTest(answers);
            return mark;
        }
    }
}
