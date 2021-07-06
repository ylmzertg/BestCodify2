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
    public class CourseOrderInfoRepository : ICourseOrderInfoRepository
    {
        private readonly BestCodifyContext _dbContext;
        private readonly IMapper _mapper;

        public CourseOrderInfoRepository(BestCodifyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<CourseOrderInfoDto>> Create(CourseOrderInfoDto details)
        {
            try
            {
                var courseOrder = _mapper.Map<CourseOrderInfoDto, CourseOrderInfo>(details);
                courseOrder.Status = ResultConstant.Status_Pending;
                var result = await _dbContext.CourseOrderInfo.AddAsync(courseOrder);
                await _dbContext.SaveChangesAsync();
                var returnData = _mapper.Map<CourseOrderInfo, CourseOrderInfoDto>(result.Entity);
                return new Result<CourseOrderInfoDto>(true, ResultConstant.RecordCreateSuccessfully, returnData);
            }
            catch (Exception e)
            {
                return new Result<CourseOrderInfoDto>(false, e.Message.ToString());
            }
        }

        public async Task<Result<CourseOrderInfoDto>> GetCourseOrderInfo(int courseId)
        {
            try
            {
                var data = await _dbContext.CourseOrderInfo
                            .Include(u => u.Course)
                            .ThenInclude(c => c.CourseImages)
                            .FirstOrDefaultAsync(u => u.Id == courseId);
                var info = _mapper.Map<CourseOrderInfo, CourseOrderInfoDto>(data);
                info.TotalCount =  _dbContext.Courses.Where(x => x.Id == courseId).ToList().Count;
                return new Result<CourseOrderInfoDto>(true, ResultConstant.RecordFound, info);
            }
            catch (Exception ex)
            {
                return new Result<CourseOrderInfoDto>(false, ex.Message.ToString());
            }
        }

        public Task<Result<CourseOrderInfoDto>> PaymentSuccessful(CourseOrderInfoDto details)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UpdateCourseOrderStatus(int courseOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
