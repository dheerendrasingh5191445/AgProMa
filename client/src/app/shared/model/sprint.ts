export class Sprint {
    constructor(
        public projectId: number,
        public sprintGoal: string,
        public sprintName: string,
        public totalDays: number,
        public startDate: string,
<<<<<<< HEAD
        public status: string,
        public expectedEndDate : string,
        public actualEndDate?:string,
=======
        public status: number,
>>>>>>> 47862963e0c31fe90db74f64f02ee07d19d0d6c7
        public sprintId?: number,
        public releasePlanId?: number,
        public expectedEndDate?:Date,
        public actualEndDate?:Date
    ) { }
}