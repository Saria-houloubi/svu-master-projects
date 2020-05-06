using SVU.Database.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVU.Database.Models
{
    /// <summary>
    /// The course that the student takes in a <see cref="Program"/>
    /// </summary>
    [Table(name: "Courses", Schema = "SVU")]
    public class Course : BaseEntityModel
    {
        #region Properties
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// the id of the program this course belongs to
        /// </summary>
        [ForeignKey(nameof(Program))]
        public Guid ProgramId { get; set; }
        #endregion

        #region Navigation Properties
        public Program Program { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<Homework> Homeworks { get; set; }
        public IEnumerable<ExternalLink> Links { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public Course()
        {
            //Create the list
            Sessions = new Collection<Session>();
            Homeworks = new Collection<Homework>();
            Links = new Collection<ExternalLink>();
        }
        #endregion
    }
}
