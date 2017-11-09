export class Sprint {
    constructor(
        public projectId: number,
        public sprintGoal: string,
        public sprintName: string,
        public totalDays: number,
        public startDate: string,
        public status: number,
        public sprintId?: number,
        public releasePlanId?: number,
        public expectedEndDate?:Date,
        public actualEndDate?:Date
    ) { }
}