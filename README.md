**Football Season Simulator**

This application simulates a season of NFL football. The program generates a season schedule, then simulates each week of the season using the schedule, preconfigured power levels for the teams in the league and a measure of random chance. The results are displayed 
week-by-week with a summary of results and updated divisional standings. 

After 18 weeks of regular season, the playoffs are seeded based on division winners and 3 wildcards. Seed 1 gets a bye for the Wildcard Round. This round is followed by the Divisional and Conference Rounds, and then ends with the Superbowl.

**Planned Features**
- Refactoring matchups to utilize approximation of real league rules
- Pull power levels from a configuration file
- After each week, a team can be searched for. The team's current record and standings are shown, along with their schedule.

**Planned Code Improvements**
- Refactor to decrease Simulator class size
- Introduce Dependency Injection
