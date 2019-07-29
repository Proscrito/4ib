using System;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Solana.Web.Admin.BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.View;
using Solana.Web.Admin.Models.Requests.FeedingFigures;
using Solana.Web.Admin.Models.Responses.FeedingFigures;
using Solana.Web.Admin.Models.Responses.FeedingFigures.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class FeedingFiguresLogic : IFeedingFiguresLogic
    {
        private readonly ISolanaRepository _repo;
        private readonly IMapper _autoMapper;

        public FeedingFiguresLogic(ISolanaRepository repo, IMapper autoMapper)
        {
            _repo = repo;
            _autoMapper = autoMapper;
        }

        public async Task<GetFeedingFigureListResponse> GetFeedingFigures()
        {
            var data = await _repo.GetListAsync<view_AdmFeedingFigures>();
            return new GetFeedingFigureListResponse
            {
                FeedingFigures = _autoMapper.Map<List<FeedingFigure>>(data)
            };
        }

        public async Task<int> SaveFeedingFigure(PostFeedingFiguresRequest request)
        {
            if (request.FeedingNumber != null)
            {
                request.FeedingNumber = Convert.ToInt32(request.FeedingNumber);
            }

            var figures = await _repo.FindAsync<AdmSitesFeedingFigure>(request.AdmSiteId);

            if (figures == null)
            {
                //insert 
                AdmSitesFeedingFigure newFigures = new AdmSitesFeedingFigure { AdmSiteID = request.AdmSiteId };
                switch (request.FeedingName)
                {
                    case "Breakfast":
                        newFigures.Breakfast = request.FeedingNumber;
                        break;
                    case "Lunch":
                        newFigures.Lunch = request.FeedingNumber;
                        break;
                    case "Snack":
                        newFigures.Snack = request.FeedingNumber;
                        break;
                    case "Supper":
                        newFigures.Supper = request.FeedingNumber;
                        break;
                }

                await _repo.CreateAsync(newFigures);
                return newFigures.AdmSiteID;
            }
            else
            {
                //update
                switch (request.FeedingName)
                {
                    case "Breakfast":
                        figures.Breakfast = request.FeedingNumber;
                        break;
                    case "Lunch":
                        figures.Lunch = request.FeedingNumber;
                        break;
                    case "Snack":
                        figures.Snack = request.FeedingNumber;
                        break;
                    case "Supper":
                        figures.Supper = request.FeedingNumber;
                        break;
                }

                await _repo.UpdateAsync(figures);
                return figures.AdmSiteID;
            }
        }
    }
}
