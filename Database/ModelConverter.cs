using Banq.Authentication;
using Banq.Database.Entities;
using Banq.DTOs;
using Banq.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                PictureURL = school.PictureURL
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
                PictureURL = school.PictureURL
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
                    Id = (ulong)id,
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

        public static FieldViewModel ToFieldViewModel(this Field field)
        {
            return new FieldViewModel
            {
                Code = field.Code,
                Name = field.Name,
            };
        }

        public static Field ToField(this FieldDTO field)
        {
            return new Field
            {
                Code = field.Code,
                Name = field.Name,
            };
        }


        public static QuestionViewModel ToQuestionViewModel(this Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                QuestionSet = question.QuestionSet,
                Content = question.Content,
            };
        }

        public async static Task<Question> ToQuestion(this QuestionDTO question, DatabaseContext databaseContext)
        {
            return new Question
            {
                QuestionSet = await databaseContext.QuestionSets.FindAsync(question.QuestionSetId),
                Content = question.Content,
            };
        }



        public static QuestionSetViewModel ToQuestionSetViewModel(this QuestionSet questionSet)
        {
            return new QuestionSetViewModel
            {
                Id = questionSet.Id,
                Level = questionSet.Level,
                Time = questionSet.Time,
                Type = questionSet.Type,
                Field = questionSet.Field,
                Grade = questionSet.Grade,
                Lesson = questionSet.Lesson,
                Author = questionSet.Author,
                Status = questionSet.Status,
                Name = questionSet.Name
            };
        }

        public static async Task<QuestionSet> ToQuestionSet(this QuestionSetDTO questionSet, DatabaseContext databaseContext, ApplicationUser author, ulong id = 0)
        {
            if (id == 0)
            {
                return new QuestionSet
                {
                    Level = questionSet.Level,
                    Time = questionSet.Time,
                    Type = questionSet.Type,
                    Field = await databaseContext.Fields.Where(x => x.Name == questionSet.FieldName).FirstAsync(),
                    Lesson = await databaseContext.Lessons.Where(x => x.Name == questionSet.LessonName).FirstAsync(),
                    Grade = questionSet.Grade,
                    Author = author,
                    Name = questionSet.Name
                };
            }
            else
            {
                return new QuestionSet
                {
                    Id = (ulong)id,
                    Level = questionSet.Level,
                    Time = questionSet.Time,
                    Type = questionSet.Type,
                    Field = await databaseContext.Fields.Where(x => x.Name == questionSet.FieldName).FirstAsync(),
                    Lesson = await databaseContext.Lessons.Where(x => x.Name == questionSet.LessonName).FirstAsync(),
                    Grade = questionSet.Grade,
                    Author = author,
                    Name = questionSet.Name
                };
            }
        }
    }
}