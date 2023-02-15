import React,{Component} from 'react';
import {variables} from './Variables.js';

export class Shqip extends Component{

    constructor(props){
        super(props);

        this.state={
            Imdbs:[],
            Shqips:[],
            modalTitle:"",
            ShqipId:0,
            ShqipName:"",
            Imdb:"",
            DateOfRelease:"",
            PhotoFileName:"anonymous.png",
            PhotoPath:variables.PHOTO_URL
        }
    }

    refreshList(){

        fetch(variables.API_URL+'Shqip')
        .then(response=>response.json())
        .then(data=>{
            this.setState({Shqips:data});
        });

        fetch(variables.API_URL+'Imdb')
        .then(response=>response.json())
        .then(data=>{
            this.setState({Imdbs:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }
    
    changeShqipName =(e)=>{
        this.setState({ShqipName:e.target.value});
    }
    changeImdb =(e)=>{
        this.setState({Imdb:e.target.value});
    }
    changeDateOfRelease =(e)=>{
        this.setState({DateOfRelease:e.target.value});
    }

    addClick(){
        this.setState({
            modalTitle:"Add Shqip",
            ShqipId:0,
            ShqipName:"",
            Imdb:"",
            DateOfRelease:"",
            PhotoFileName:"anonymous.png"
        });
    }
    editClick(sqp){
        this.setState({
            modalTitle:"Edit Shqip",
            ShqipId:sqp.ShqipId,
            ShqipName:sqp.ShqipName,
            Imdb:sqp.Imdb,
            DateOfRelease:sqp.DateOfRelease,
            PhotoFileName:sqp.PhotoFileName
        });
    }

    createClick(){
        fetch(variables.API_URL+'Shqip',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                ShqipName:this.state.ShqipName,
                Imdb:this.state.Imdb,
                DateOfRelease:this.state.DateOfRelease,
                PhotoFileName:this.state.PhotoFileName
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }


    updateClick(){
        fetch(variables.API_URL+'Shqip',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                ShqipId:this.state.ShqipId,
                ShqipName:this.state.ShqipName,
                Imdb:this.state.Imdb,
                DateOfRelease:this.state.DateOfRelease,
                PhotoFileName:this.state.PhotoFileName
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    deleteClick(id){
        if(window.confirm('Are you sure?')){
        fetch(variables.API_URL+'Shqip/'+id,{
            method:'DELETE',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
        }
    }

    imageUpload=(e)=>{
        e.preventDefault();

        const formData=new FormData();
        formData.append("file",e.target.files[0],e.target.files[0].name);

        fetch(variables.API_URL+'Shqip/savefile',{
            method:'POST',
            body:formData
        })
        .then(res=>res.json())
        .then(data=>{
            this.setState({PhotoFileName:data});
        })
    }

    render(){
        const {
            Imdbs,
            Shqips,
            modalTitle,
            ShqipId,
            ShqipName,
            Imdb,
            DateOfRelease,
            PhotoPath,
            PhotoFileName
        }=this.state;

        return(
<div>

    <button type="button"
    className="btn btn-primary m-2 float-end"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    onClick={()=>this.addClick()}>
        Add Shqip
    </button>
    <table className="table table-striped">
    <thead>
    <tr>
        <th>
            ShqipId
        </th>
        <th>
            ShqipName
        </th>
        <th>
            Imdb
        </th>
        <th>
            DOR
        </th>
        <th>
            Options
        </th>
    </tr>
    </thead>
    <tbody>
        {Shqips.map(sqp=>
            <tr key={sqp.ShqipId}>
                <td>{sqp.ShqipId}</td>
                <td>{sqp.ShqipName}</td>
                <td>{sqp.Imdb}</td>
                <td>{sqp.DateOfRelease}</td>
                <td>
                <button type="button"
                className="btn btn-light mr-1"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={()=>this.editClick(sqp)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                    <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                    </svg>
                </button>

                <button type="button"
                className="btn btn-light mr-1"
                onClick={()=>this.deleteClick(sqp.ShqipId)}>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                    <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                    </svg>
                </button>

                </td>
            </tr>
            )}
    </tbody>
    </table>

<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
<div className="modal-dialog modal-lg modal-dialog-centered">
<div className="modal-content">
   <div className="modal-header">
       <h5 className="modal-title">{modalTitle}</h5>
       <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"
       ></button>
   </div>

   <div className="modal-body">
    <div className="d-flex flex-row bd-highlight mb-3">
     
     <div className="p-2 w-50 bd-highlight">
    
        <div className="input-group mb-3">
            <span className="input-group-text">Shqip Name</span>
            <input type="text" className="form-control"
            value={ShqipName}
            onChange={this.changeShqipName}/>
        </div>

        <div className="input-group mb-3">
            <span className="input-group-text">Imdb</span>
            <select className="form-select"
            onChange={this.changeImdb}
            value={Imdb}>
                {Imdbs.map(Im=><option key={Im.ImdbId}>
                    {Im.ImdbValue}
                </option>)}
            </select>
        </div>

        <div className="input-group mb-3">
            <span className="input-group-text">DOR</span>
            <input type="date" className="form-control"
            value={DateOfRelease}
            onChange={this.changeDateOfRelease}/>
        </div>


     </div>
     <div className="p-2 w-50 bd-highlight">
         <img width="250px" height="250px"
         src={PhotoPath+PhotoFileName}/>
         <input className="m-2" type="file" onChange={this.imageUpload}/>
     </div>
    </div>

    {ShqipId==0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.createClick()}
        >Create</button>
        :null}

        {ShqipId!=0?
        <button type="button"
        className="btn btn-primary float-start"
        onClick={()=>this.updateClick()}
        >Update</button>
        :null}
   </div>

</div>
</div> 
</div>


</div>
        )
    }
}