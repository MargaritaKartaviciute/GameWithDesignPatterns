import React from 'react';
import axiosInstance from './helpers/AxiosConfig';

export default class StartGame extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      trees: 50,
      rocks: 10,
      water: 30,
      maxX: props.defaultX,
      maxY: props.defaultY,
      username: "",
    }
  }

  render = () => {
    return (
      <div style={{display: "flex", flexDirection: "column", width: 200, alignItems: "center"}}>
          <React.Fragment>
            Username:
            <input value={this.state.username}
              type="text"
              onChange={(e) => this.setState({...this.state, username: e.target.value})}/>
          </React.Fragment>
          {["Trees", "Rocks", "Water"].map((e, i) => 
            <React.Fragment key={i}>
              {e} count:  
              <input value={this.state[e.toLowerCase()]} 
              type="number" 
              onChange={(event) => this.setState({...this.state, [e.toLowerCase()]: event.target.value})}/>
            </React.Fragment>
          )}
          {["X", "Y"].map((e, i) => 
          <React.Fragment key={i}>
            Max{e}:
            <input value={this.state['max' + e]}
              type="number"
              onChange={(event) => this.setState({...this.state, ['max' + e]: event.target.value})}/>
          </React.Fragment>)}
        <button 
          onClick={() => 
            axiosInstance
              .post("maps/", this.state)
              .then(res => {
                this.props.setUsername(this.state.username);
              })
            }
          >Start game
        </button>
      </div>
    )
  }
}
