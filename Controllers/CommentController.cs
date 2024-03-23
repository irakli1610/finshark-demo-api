using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IcommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(IcommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo= commentRepo;
            _stockRepo = stockRepo;
        }


        [HttpGet]
        public async Task<ActionResult> GetAll(){

            

            var comments = await _commentRepo.GetAllAsync();

            var CommentDto = comments.Select(s => s.ToCommentDto());

            return Ok(CommentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            ;
            
            var comment = await _commentRepo.GetById(id);

            if (comment == null)  
                return NotFound(); 
            
            return Ok(comment.ToCommentDto());
        }


        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);


            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("stock does not exist");
            }

            var CommentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(CommentModel);
            return CreatedAtAction(nameof(GetById), new{id = CommentModel.Id}, CommentModel.ToCommentDto());
        }
    
    
    
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel =  await _commentRepo.DeleteAsync(id);

            if(commentModel == null){
                return NotFound("Comment does not exist");
            } 

            return Ok(commentModel); 
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCommentRequestDto updateDto)
        {
            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate()); 

            if(comment==null)
                NotFound("Comment not found");

            return Ok(comment.ToCommentDto());
        
        }
    
    }
}