﻿using MyNeo4j.model;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Service
{
    public interface IReleasePlanService    //Interface
    {
        List<ReleasePlanMaster> GetAllReleasePlan();
        void AddRelease(ReleasePlanMaster releasePlan);
        List<SprintBacklog> GetAllSprints(int id);
    }
    public class ReleasePlanService : IReleasePlanService
    {
        private IReleasePlanRepo _releasePlanRepo;
        private ISprintRepository _sprintRepository;
        public ReleasePlanService(IReleasePlanRepo releasePlanRepo, ISprintRepository sprintRepository) //Constructor
        {
            _releasePlanRepo = releasePlanRepo;
            _sprintRepository = sprintRepository;
        }

        //Method for adding a new release
        public void AddRelease(ReleasePlanMaster releasePlan)
        {
            _releasePlanRepo.AddRelease(releasePlan);
        }

        //Method for getting a list of all release
        public List<ReleasePlanMaster> GetAllReleasePlan()
        {
            return _releasePlanRepo.GetAllRelease();
        }

        //Method for getting the list of all sprints
        public List<SprintBacklog> GetAllSprints(int id)
        {
            return _sprintRepository.GetAll(id);
        }
    }
}