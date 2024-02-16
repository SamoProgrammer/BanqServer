using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banq.Authentication;
using Banq.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace Banq.Database
{
    public static class ModelConverter
    {
        public static SchoolViewModel ToSchoolViewModel(this School school)
        {
            return new SchoolViewModel
            {
                Code = school.Code,
                CourseLevel = school.CourseLevel,
                Gender = school.Gender,
                Name = school.Name,
                PictureGuid = school.PictureGuid
            };
        }

        public static School ToSchool(this SchoolDTO school)
        {
            return new School
            {
                Code = school.Code,
                CourseLevel = school.CourseLevel,
                Gender = school.Gender,
                Name = school.Name,
                PictureGuid = school.PictureGuid
            };
        }

        public static CommentViewModel ToCommentViewModel(this Comment comment)
        {
            return new CommentViewModel
            {
                Content = comment.Content,
                Likes = comment.Likes,
                Username = comment.User.UserName
            };
        }

        public static async Task<Comment> ToComment(this CommentDTO comment, ulong? id, UserManager<ApplicationUser> userManager)
        {
            if (id == null)
            {
                return new Comment
                {
                    Content = comment.Content,
                    Likes = comment.Likes,
                    User = await userManager.FindByNameAsync(comment.Username)
                };
            }
            else
            {
                return new Comment
                {
                    Id=(ulong)id,
                    Content = comment.Content,
                    Likes = comment.Likes,
                    User = await userManager.FindByNameAsync(comment.Username)
                };
            }
        }

        public static LessonViewModel ToLessonViewModel(this Lesson lesson)
        {
            return new LessonViewModel
            {
                Code = lesson.Code,
                Name = lesson.Name,
                BookCode = lesson.BookCode,
            };
        }

        public static Lesson ToLesson(this LessonDTO lesson)
        {
            return new Lesson
            {
                Code = lesson.Code,
                Name = lesson.Name,
                BookCode = lesson.BookCode,
            };
        }


        public static QuestionViewModel ToQuestionViewModel(this Question question)
        {
            return new QuestionViewModel
            {
                Level = question.Level,
                Time = question.Time,
                Type = question.Type
            };
        }

        public static Question ToQuestion(this QuestionDTO question, ulong? id)
        {
            if (id == null)
            {
                return new Question
                {
                    Level = question.Level,
                    Time = question.Time,
                    Type = question.Type
                };
            }
            else
            {
                return new Question
                {
                    Id = (ulong)id,
                    Level = question.Level,
                    Time = question.Time,
                    Type = question.Type
                };
            }
        }
    }
}