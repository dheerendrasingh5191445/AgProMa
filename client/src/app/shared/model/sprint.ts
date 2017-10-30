export class Sprint {
    constructor(
        public projectId: number,
        public sprintGoal: string,
        public sprintName: string,
        public totalDays: number,
        public startDate: string,
        public status: boolean,
        public sprintId?: number,
        public releasePlanId?: number
    ) { }
}