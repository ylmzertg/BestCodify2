using AutoMapper;
using BestCodify.Business.Repository.IRepository;
using BestCodify.Common;
using BestCodify.DataAccess.Data;
using BestCodify.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCodify.Business
{
    public class CourseRepository : ICourseRepository
    {
        private readonly BestCodifyContext _dbContext;
        private readonly IMapper _mapper;

        public CourseRepository(BestCodifyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<CourseDto>> CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<CourseDto, Course>(courseDto);
            course.CreatedDate = DateTime.Now;
            course.CreatedBy = string.Empty;
            var addedCourse = await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            var returnData = _mapper.Map<Course, CourseDto>(addedCourse.Entity);
            return new Result<CourseDto>(true, ResultConstant.RecordCreateSuccessfully, returnData);
        }

        public async Task<Result<int>> DeleteCourse(int courseId)
        {
            var courseDetails = await _dbContext.Courses.FindAsync(courseId);
            if (courseDetails != null)
            {
                _dbContext.Courses.Remove(courseDetails);
                var result = await _dbContext.SaveChangesAsync();
                return new Result<int>(true, ResultConstant.RecordRemoveSuccessfully, result);
            }
            return new Result<int>(false, ResultConstant.RecordRemoveNotSuccessfully);
        }

        public async Task<Result<IEnumerable<CourseDto>>> GetAllCourse()
        {
            try
            {
                var courseDtos =
                            _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(_dbContext.Courses.Include(x => x.CourseImages));

                return new Result<IEnumerable<CourseDto>>(true, ResultConstant.RecordFound, courseDtos, courseDtos.ToList().Count);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<CourseDto>>(false, ResultConstant.RecordNotFound);
            }
        }

        public async Task<Result<CourseDto>> GetCourse(int courseId)
        {
            try
            {
                var data = await _dbContext.Courses.Include(x => x.CourseImages)
                                                    .FirstOrDefaultAsync(x => x.Id == courseId);
                var returnData = _mapper.Map<Course, CourseDto>(data);
                return new Result<CourseDto>(true, ResultConstant.RecordFound, returnData);
            }
            catch (Exception ex)
            {
                return new Result<CourseDto>(false, ResultConstant.RecordNotFound);
            }
        }

        /// <summary>
        ///if unique returns null else returns the course obj
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Result<CourseDto>> IsCourseUnique(string name, int courseId = 0)
        {
            try
            {
                if (courseId == 0)
                {
                    var data = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                    var returnData = _mapper.Map<Course, CourseDto>(data);
                    return new Result<CourseDto>(true, ResultConstant.RecordFound, returnData);
                }
                else
                {
                    var data = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != courseId);
                    var returnData = _mapper.Map<Course, CourseDto>(data);
                    return new Result<CourseDto>(true, ResultConstant.RecordFound, returnData);
                }

            }
            catch (Exception ex)
            {
                return new Result<CourseDto>(false, ResultConstant.RecordNotFound);
            }
        }

        public async Task<Result<CourseDto>> UpdateCourse(int courseId, CourseDto courseDto)
        {
            try
            {
                if (courseId == courseDto.Id)
                {
                    //valid
                    var courseDetails = await _dbContext.Courses.FindAsync(courseId);
                    var course = _mapper.Map<CourseDto, Course>(courseDto, courseDetails);
                    course.UpdatedBy = string.Empty;
                    course.UpdatedDate = DateTime.Now;
                    var updateCourse = _dbContext.Courses.Update(course);
                    await _dbContext.SaveChangesAsync();
                    var returnData = _mapper.Map<Course, CourseDto>(updateCourse.Entity);
                    return new Result<CourseDto>(true, ResultConstant.RecordUpdateSuccessfully, returnData);
                }
                else
                    return new Result<CourseDto>(false, ResultConstant.RecordUpdateNotSuccessfully);
            }
            catch (Exception ex)
            {
                return new Result<CourseDto>(false, ResultConstant.RecordUpdateNotSuccessfully);
            }
        }
    }
}
