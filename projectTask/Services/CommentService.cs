using Microsoft.EntityFrameworkCore;
using projectTask.Models;
using projectTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectTask.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentsGetModels> getCommentswithfilter(string filter);
    }

    public class CommentService : ICommentService
    {
        private TasksDbContext taskDbcontext;
        public CommentService (TasksDbContext context)
        {
            this.taskDbcontext = context;
        }

        public IEnumerable<CommentsGetModels> getCommentswithfilter(string filter)
        {
            IEnumerable<Task> all = taskDbcontext.Tasks.Include(t => t.Comments);
            CommentsGetModels comments;
            List<CommentsGetModels> resultEnum = new List<CommentsGetModels>();
            foreach (Task task in all)
            {
                foreach (Comment comment in task.Comments)
                {
                    if (comment.Text.Contains(filter))
                    {
                        comments = new CommentsGetModels();
                        comments.Id = comment.Id;
                        comments.Text = comment.Text;
                        comments.Important = comment.Important;
                        comments.IdTask = task.Id;
                        resultEnum.Add(comments);
                    }
                }
            }
            IEnumerable<CommentsGetModels> send = resultEnum;
            return send;
        }
    }
}
