import React from "react";
import axios from "axios";
import StartGame from "./StartGame";
import axiosInstance from "./helpers/AxiosConfig";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
const MySwal = withReactContent(Swal);

export default class Mainpage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      gridCellSize: 32,
      gridCells: 70,
      gridCells2: 35,
      username: "",
      players: [],
      tempUsername: "",
      movingPlayer: null,
      items: [],
    };
    this.map = [];
    this.lastInfo = null;
    this.timer = window.setInterval(
      () => axiosInstance.get("/maps").then(res => this.handleResponse(res)),
      20000
    );
  }

  handleResponse = res => {
    this.map = [];
    res.data.mapObjects.forEach(e => {
      this.map.push({
        x: e.x,
        y: e.y,
        type: e.type,
      });
    });
    res.data.players.forEach(e => {
      this.map.push({
        x: e.x,
        y: e.y,
        username: e.userName,
        type: "player",
      });
    });
    this.setState({
      ...this.state,
      gridCells: res.data.maxX,
      gridCells2: res.data.maxY,
      players: res.data.players,
      movingPlayer: res.data.movingPlayer,
    });
    this.updateCanvas();
  };

  handleError = res => {
    MySwal.fire(
      {
        title: "Action unavailable!",
        type: "error",
        confirmButtonColor: "red",
        text: res && res.response && res.response.data ? res.response.data.messageText:null,
        timer: 1200,
        nBeforeOpen: () => {
          Swal.showLoading();
        },
      },
      function() {
        MySwal.close();
      }
    );
  };

  componentDidMount = () => {};

  componentWillMount = () => {
    axiosInstance.get("/maps").then(res => this.handleResponse(res));
    axiosInstance
      .get("/items")
      .then(res =>
        this.setState((state, props) => ({ ...state, items: res.data }))
      );
  };

  componentDidUpdate = () => {};

  handleCanvasClick = e => {
    const { x, y } = this.getGridPosition(e);
    if (this.getPlayer()) {
      axiosInstance
        .put("/players/" + this.getPlayer().userName, {
          x,
          y,
        })
        .then(res => this.handleResponse(res, true))
        .catch(res => this.handleError(res, true));
    }
  };

  getPlayer = () => {
    if (
      this.state.username &&
      this.state.players.filter(e => e.userName === this.state.username)
    ) {
      return this.state.players.filter(
        e => e.userName === this.state.username
      )[0];
    }
    return null;
  };

  drawGrid = () => {
    const ctx = this.refs.canvas.getContext("2d");
    ctx.fillStyle = "rgb(200,200,200)";
    ctx.fillRect(
      0,
      0,
      this.state.gridCells * this.state.gridCellSize,
      this.state.gridCells2 * this.state.gridCellSize
    );
    ctx.fillStyle = "rgb(0, 0, 0)";
    for (let i = 0; i < this.state.gridCells; i++) {
      ctx.beginPath();
      ctx.moveTo(i * this.state.gridCellSize + 1, 0);
      ctx.lineTo(
        i * this.state.gridCellSize + 1,
        this.state.gridCells2 * this.state.gridCellSize
      );
      ctx.stroke();

      ctx.beginPath();
      ctx.moveTo((i + 1) * this.state.gridCellSize - 1, 0);
      ctx.lineTo(
        (i + 1) * this.state.gridCellSize - 1,
        this.state.gridCells2 * this.state.gridCellsSize
      );
      ctx.stroke();
    }

    for (let i = 0; i < this.state.gridCells2; i++) {
      ctx.beginPath();
      ctx.moveTo(0, i * this.state.gridCellSize + 1);
      ctx.lineTo(
        this.state.gridCells * this.state.gridCellSize,
        i * this.state.gridCellSize + 1
      );
      ctx.stroke();

      ctx.beginPath();
      ctx.moveTo(0, (i + 1) * this.state.gridCellSize - 1);
      ctx.lineTo(
        this.state.gridCells * this.state.gridCellsSize,
        (i + 1) * this.state.gridCellSize - 1
      );
      ctx.stroke();
    }
  };

  updateCanvas = () => {
    if (!this.gameStarted()) {
      console.log("Game hasnt started yet!");
      return;
    }
    if (this.lastInfo && JSON.stringify(this.map) === this.lastInfo) {
      console.log("NO UPDATE");
      return;
    }
    console.log(JSON.stringify(this.map), this.lastInfo);
    this.drawGrid();
    const ctx = this.refs.canvas.getContext("2d");
    this.map.forEach(e => {
      if (e.type === "cursor") {
        ctx.fillStyle = "#FF0000";
        ctx.beginPath();
        ctx.arc(
          e.x * this.state.gridCellSize + this.state.gridCellSize / 2,
          e.y * this.state.gridCellSize + this.state.gridCellSize / 2,
          10,
          0,
          2 * Math.PI
        );
        ctx.fill();
      } else if (e.type === 3) {
        var image = new Image();
        image.onload = () => {
          ctx.drawImage(
            image,
            e.x * this.state.gridCellSize,
            e.y * this.state.gridCellSize
          );
        };
        image.src =
          "data:image/png;base64,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";
      } else if (e.type === 2) {
        var image = new Image();
        image.onload = () => {
          ctx.drawImage(
            image,
            e.x * this.state.gridCellSize,
            e.y * this.state.gridCellSize
          );
        };
        image.src =
          "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAAsTAAALEwEAmpwYAAAKT2lDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAHjanVNnVFPpFj333vRCS4iAlEtvUhUIIFJCi4AUkSYqIQkQSoghodkVUcERRUUEG8igiAOOjoCMFVEsDIoK2AfkIaKOg6OIisr74Xuja9a89+bN/rXXPues852zzwfACAyWSDNRNYAMqUIeEeCDx8TG4eQuQIEKJHAAEAizZCFz/SMBAPh+PDwrIsAHvgABeNMLCADATZvAMByH/w/qQplcAYCEAcB0kThLCIAUAEB6jkKmAEBGAYCdmCZTAKAEAGDLY2LjAFAtAGAnf+bTAICd+Jl7AQBblCEVAaCRACATZYhEAGg7AKzPVopFAFgwABRmS8Q5ANgtADBJV2ZIALC3AMDOEAuyAAgMADBRiIUpAAR7AGDIIyN4AISZABRG8lc88SuuEOcqAAB4mbI8uSQ5RYFbCC1xB1dXLh4ozkkXKxQ2YQJhmkAuwnmZGTKBNA/g88wAAKCRFRHgg/P9eM4Ors7ONo62Dl8t6r8G/yJiYuP+5c+rcEAAAOF0ftH+LC+zGoA7BoBt/qIl7gRoXgugdfeLZrIPQLUAoOnaV/Nw+H48PEWhkLnZ2eXk5NhKxEJbYcpXff5nwl/AV/1s+X48/Pf14L7iJIEyXYFHBPjgwsz0TKUcz5IJhGLc5o9H/LcL//wd0yLESWK5WCoU41EScY5EmozzMqUiiUKSKcUl0v9k4t8s+wM+3zUAsGo+AXuRLahdYwP2SycQWHTA4vcAAPK7b8HUKAgDgGiD4c93/+8//UegJQCAZkmScQAAXkQkLlTKsz/HCAAARKCBKrBBG/TBGCzABhzBBdzBC/xgNoRCJMTCQhBCCmSAHHJgKayCQiiGzbAdKmAv1EAdNMBRaIaTcA4uwlW4Dj1wD/phCJ7BKLyBCQRByAgTYSHaiAFiilgjjggXmYX4IcFIBBKLJCDJiBRRIkuRNUgxUopUIFVIHfI9cgI5h1xGupE7yAAygvyGvEcxlIGyUT3UDLVDuag3GoRGogvQZHQxmo8WoJvQcrQaPYw2oefQq2gP2o8+Q8cwwOgYBzPEbDAuxsNCsTgsCZNjy7EirAyrxhqwVqwDu4n1Y8+xdwQSgUXACTYEd0IgYR5BSFhMWE7YSKggHCQ0EdoJNwkDhFHCJyKTqEu0JroR+cQYYjIxh1hILCPWEo8TLxB7iEPENyQSiUMyJ7mQAkmxpFTSEtJG0m5SI+ksqZs0SBojk8naZGuyBzmULCAryIXkneTD5DPkG+Qh8lsKnWJAcaT4U+IoUspqShnlEOU05QZlmDJBVaOaUt2ooVQRNY9aQq2htlKvUYeoEzR1mjnNgxZJS6WtopXTGmgXaPdpr+h0uhHdlR5Ol9BX0svpR+iX6AP0dwwNhhWDx4hnKBmbGAcYZxl3GK+YTKYZ04sZx1QwNzHrmOeZD5lvVVgqtip8FZHKCpVKlSaVGyovVKmqpqreqgtV81XLVI+pXlN9rkZVM1PjqQnUlqtVqp1Q61MbU2epO6iHqmeob1Q/pH5Z/YkGWcNMw09DpFGgsV/jvMYgC2MZs3gsIWsNq4Z1gTXEJrHN2Xx2KruY/R27iz2qqaE5QzNKM1ezUvOUZj8H45hx+Jx0TgnnKKeX836K3hTvKeIpG6Y0TLkxZVxrqpaXllirSKtRq0frvTau7aedpr1Fu1n7gQ5Bx0onXCdHZ4/OBZ3nU9lT3acKpxZNPTr1ri6qa6UbobtEd79up+6Ynr5egJ5Mb6feeb3n+hx9L/1U/W36p/VHDFgGswwkBtsMzhg8xTVxbzwdL8fb8VFDXcNAQ6VhlWGX4YSRudE8o9VGjUYPjGnGXOMk423GbcajJgYmISZLTepN7ppSTbmmKaY7TDtMx83MzaLN1pk1mz0x1zLnm+eb15vft2BaeFostqi2uGVJsuRaplnutrxuhVo5WaVYVVpds0atna0l1rutu6cRp7lOk06rntZnw7Dxtsm2qbcZsOXYBtuutm22fWFnYhdnt8Wuw+6TvZN9un2N/T0HDYfZDqsdWh1+c7RyFDpWOt6azpzuP33F9JbpL2dYzxDP2DPjthPLKcRpnVOb00dnF2e5c4PziIuJS4LLLpc+Lpsbxt3IveRKdPVxXeF60vWdm7Obwu2o26/uNu5p7ofcn8w0nymeWTNz0MPIQ+BR5dE/C5+VMGvfrH5PQ0+BZ7XnIy9jL5FXrdewt6V3qvdh7xc+9j5yn+M+4zw33jLeWV/MN8C3yLfLT8Nvnl+F30N/I/9k/3r/0QCngCUBZwOJgUGBWwL7+Hp8Ib+OPzrbZfay2e1BjKC5QRVBj4KtguXBrSFoyOyQrSH355jOkc5pDoVQfujW0Adh5mGLw34MJ4WHhVeGP45wiFga0TGXNXfR3ENz30T6RJZE3ptnMU85ry1KNSo+qi5qPNo3ujS6P8YuZlnM1VidWElsSxw5LiquNm5svt/87fOH4p3iC+N7F5gvyF1weaHOwvSFpxapLhIsOpZATIhOOJTwQRAqqBaMJfITdyWOCnnCHcJnIi/RNtGI2ENcKh5O8kgqTXqS7JG8NXkkxTOlLOW5hCepkLxMDUzdmzqeFpp2IG0yPTq9MYOSkZBxQqohTZO2Z+pn5mZ2y6xlhbL+xW6Lty8elQfJa7OQrAVZLQq2QqboVFoo1yoHsmdlV2a/zYnKOZarnivN7cyzytuQN5zvn//tEsIS4ZK2pYZLVy0dWOa9rGo5sjxxedsK4xUFK4ZWBqw8uIq2Km3VT6vtV5eufr0mek1rgV7ByoLBtQFr6wtVCuWFfevc1+1dT1gvWd+1YfqGnRs+FYmKrhTbF5cVf9go3HjlG4dvyr+Z3JS0qavEuWTPZtJm6ebeLZ5bDpaql+aXDm4N2dq0Dd9WtO319kXbL5fNKNu7g7ZDuaO/PLi8ZafJzs07P1SkVPRU+lQ27tLdtWHX+G7R7ht7vPY07NXbW7z3/T7JvttVAVVN1WbVZftJ+7P3P66Jqun4lvttXa1ObXHtxwPSA/0HIw6217nU1R3SPVRSj9Yr60cOxx++/p3vdy0NNg1VjZzG4iNwRHnk6fcJ3/ceDTradox7rOEH0x92HWcdL2pCmvKaRptTmvtbYlu6T8w+0dbq3nr8R9sfD5w0PFl5SvNUyWna6YLTk2fyz4ydlZ19fi753GDborZ752PO32oPb++6EHTh0kX/i+c7vDvOXPK4dPKy2+UTV7hXmq86X23qdOo8/pPTT8e7nLuarrlca7nuer21e2b36RueN87d9L158Rb/1tWeOT3dvfN6b/fF9/XfFt1+cif9zsu72Xcn7q28T7xf9EDtQdlD3YfVP1v+3Njv3H9qwHeg89HcR/cGhYPP/pH1jw9DBY+Zj8uGDYbrnjg+OTniP3L96fynQ89kzyaeF/6i/suuFxYvfvjV69fO0ZjRoZfyl5O/bXyl/erA6xmv28bCxh6+yXgzMV70VvvtwXfcdx3vo98PT+R8IH8o/2j5sfVT0Kf7kxmTk/8EA5jz/GMzLdsAAAAgY0hSTQAAeiUAAICDAAD5/wAAgOkAAHUwAADqYAAAOpgAABdvkl/FRgAAB7VJREFUeNrEl1tsHGcVx3/f983O7K7XXtuNnahO4lyaRrm1JGmhlF4gtBUqT5WKBEGVeIBnhMRFfYAXqIqQqCjPBUFVFalCggcopRVIqBe1aZukTWPqNrEd26kTX9a73vXu7HyXw8M6rYkdU+ChRzqakeabc37z/86ZOaNEhE/SoisnO47oj33Tzd/oNQgPA18DtqB4Cfj+W7+pjn7cGBMnAwDqigIbARx6qPc4cBwoITwrcO8tu4v3HNhZoJhozl1s88LppQZw/ztPVV/8vwEOHC8PAfcCHqF333D+l/u3F9Aazn+QMdhv6OnJsZx1gvQkmlrV8ufXlv6pNIfOPl3zB46XOyop7gfmgEfOPl07sSHAhVPClx4tPwA8eXBXoRQCjIy32g98oS/RkabZDngB64TUdY4CFGLFYMlwbqLJ2+dbQaFOBoQjNxZv2b+jwOKS49nXai3gzucerr05fFitD7D3gZ4h4P0Hv1gudHVHNFqBVgap9aQZWAfOCyKC96CqgdycQw0Y8kMx/V2GnoKi3nBYhGLBYAN0FxSzs5bnX62/8PyPavetBohW70uw8tAth4oFk2gmLluaaSAIBFGIQAjgXcAHUFbofbnJni3bmBiZpPFljQlCO1NopWg6Yb7hcEEodxm2bckRrBy754c9hff/VG+t6QKAkMnOYh4qdUe1HmimHUqjFMEFRBRxolEhIAEK847JyfNQ1NSWHI1WYPFSm5l5K0dv61Zq2pGMZqS7YxZvz1OIlFluhX7g4pWc/1b63kqlvhzIvGCzgG0LNhVsO1C57GjWHRIEUAQDU/cWqeyNmbirwOwHlomRJs7CgZtKqlnzFF9pcbe/gd5f16hd9izVfPBWfr/jWFf/+gCZPDs21kaCEOqeZDxDz1jazYDLAjPnUi6ONbl8IWXk5SXGx1Omtxvq1pPLK8qbI5IeTWPJkqYBUuH0iTNoKzSd4ytfLev9+5LbfCbfXH8LHK/MTLqXhobadySjju0McvHURaZvT1AGkm5DdcZjUwtAMmhoLXkEUBq0UZgITKQwOcX05xM2jxma+3LkNLRdICAES2FdAN+Wx/bcFN+RlBTRgmNmdhqxQmYF16lG8t2apEvhndCqOURAKVAryUNOIZFCRNHu0kzvj+jr1URGeOZXtYxABcWT6wNk8q09RyLmpwKtmyLi84FKvyLzAW+l45ngHIQMQEB3AExOAQodgRcgdNZePm9hb4yJDMEyu3Vg1w0n/362fa0aeGtm2pM5IY0V1T0RvkuBFlDgPWTZSmFmATdrsG3BeyEEAS2oGEwi6AhUrChtiqgveJwEtuzUWyenxx9cnfNDgN7huBCsVBoVR5YFFsYy2ssObYQoBzoSJHSUcDbgbEDNRngXIAjagDbC4mSGOEGbzvpW1eHanmrFs2mbxqXuid7h+PY1AN7KI1sPmvuL/dBa9tgs0FrypA1P2vTMvJmSTDriJY93gneCXswR2tBqBJbmXUcJB2NvpMyey7g40qY273EWavOeIMK+Y1HeW/nJmhrwmWTehU6PByHXBfU5T7osSFPYcjlwz/AO3h0d5/Rug0MgU1A1hHJGayHQrARAGO6PMKOetAzR1qijVkuRNYWkC3wmfWsAgpVHx191n5075+8a2K2IC5p8j2BikKLi8FswNzNGtwYngl42JEpoNQ3SA4WyBgVxrCldctx63Q6mRyd4+7qAU4q4CC4LXHpPCFaeWQOQ1nztwim5+4bPRbc6y4k9dwo2g6wtZAH+cavh+unA1KDCGyGuxwzEiprrfCfwCpMZssiTonhncoK8AashiSHpFk790QfgYeDn63bB8GGFbfrX6zPhFyPPORrzgbgEJiekSWA8zpOi0FowCzHlfITSEHlN8m6ZkHiUCsxdLywMwXtHFblSICkHZs95gpWnbNP/zDa9Xxfgo4IM32kuyP6p18Nv6zOBpFvI5UFpRfxOL7mRMntKBfIKQkNjzvYgJpDr9cQlyPXB4maIruvcNzcamHojnA9Wvnd1rg1HMnFsA05ef1htKg1CNm8ovrqJg30JRaXQPjDTDlyqLTO72RPd3CB4oToJ9Rk5DQwDi8BfgR+oiPrVE1G00dymIqZ8m2PTb8oTSTefzpcdraxNb7GLVt3SVdD0BriUi6gvZJeqL4UKUAEeMzF/+K+m4muZSTgDfCatyqG0ypFE1HejOD5YiCxbBnooNBxTyy3yzfzjprT80/95LL8iyQZ2BjhzdOemr0/PLR+sN9uoHstAIU+sNQFf+hgx1tiHG6+U+k+ulFJKYHucj8HBwmKTuaVlvHUIfEp9ZBvGWhfgCscq1ytugATo2rW5+85Yyd4be4StRY32gYnZJYzOEZD7tm/qug0oA4UVdfVVMdVGNaBWQamVawlQAgYirbblc+bxncWIv11YIglCXhTjqadlPbu6ktyoC7/rLuR+XG/Zs8AsUAVagAP86gZb04aqo82VxDmgCPQA/f3dydHBcuHbCtVnfVhspHYqnzNDhTjaVmmkJ4B2PjbdRmlS69/+oLL8F2BhpSNqwDJgV1xk1Q/pagCukt2sgORWoOJV51Eu0kl3IddXqbcvrzyRW0mQrUrmrzrKCgDXArjmK2G9/buGydUyr1mwHsAnZf8aAN0aT5Zus2t2AAAAAElFTkSuQmCC";
      } else if (e.type === 1) {
        var image = new Image();
        image.onload = () => {
          ctx.drawImage(
            image,
            e.x * this.state.gridCellSize,
            e.y * this.state.gridCellSize
          );
        };
        image.src =
          "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAACXBIWXMAAAsTAAALEwEAmpwYAAADa2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4wLWMwNjAgNjEuMTM0Nzc3LCAyMDEwLzAyLzEyLTE3OjMyOjAwICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXBSaWdodHM9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9yaWdodHMvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1wUmlnaHRzOk1hcmtlZD0iRmFsc2UiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6RkFBQTkwN0QyOEFCMTFFNTk5RDhEQkFBRTMwMUE3MEEiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6RkFBQTkwN0MyOEFCMTFFNTk5RDhEQkFBRTMwMUE3MEEiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTMyBNYWNpbnRvc2giPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0idXVpZDpFODdFM0NGRjg0MDBFMDExQUZCNUQwQTdGN0UxM0JBRSIgc3RSZWY6ZG9jdW1lbnRJRD0idXVpZDpFNzdFM0NGRjg0MDBFMDExQUZCNUQwQTdGN0UxM0JBRSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6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";
      } else if (e.type === "player") {
        var image = new Image();
        image.onload = () => {
          ctx.drawImage(
            image,
            e.x * this.state.gridCellSize,
            e.y * this.state.gridCellSize
          );
          ctx.fillStyle = "blue";
          ctx.font = "50px Arial";
          ctx.textAlign = "center";
          ctx.fillText(
            e.username,
            e.x * this.state.gridCellSize + this.state.gridCellSize / 2,
            e.y * this.state.gridCellSize
          );
        };
        image.src =
          "data:image/png;base64,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";
      } else {
        console.log("Unknown type " + e.type);
      }
    });
    this.lastInfo = JSON.stringify(this.map);
  };

  handleMouseMove = e => {
    /*const {x, y} = this.getGridPosition(e);
    this.map = this.map.filter(e => e.type !== "cursor");
    this.map.push({
      type: "cursor",
      x,
      y
    })
    //this.updateCanvas();*/
  };

  getPixelPosition = event => {
    const canvas = document.querySelector("canvas");
    const rect = canvas.getBoundingClientRect();
    const x = Math.floor(event.clientX - rect.left);
    const y = Math.floor(event.clientY - rect.top);
    return { x, y };
  };

  getGridPosition = event => {
    const canvas = document.querySelector("canvas");
    const rect = canvas.getBoundingClientRect();
    const x = Math.floor(
      Math.floor(event.clientX - rect.left) / this.state.gridCellSize
    );
    const y = Math.floor(
      Math.floor(event.clientY - rect.top) / this.state.gridCellSize
    );
    return { x, y };
  };

  gameStarted = () => {
    return this.map.length > 0;
  };

  render() {
    return (
      <div>
        <div
          style={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <h1 style={{ marginTop: "70px" }}>Žaidimas "SALA"</h1>
          <img
            style={{ marginBottom: "10%" }}
            src="data:image/jpeg;base64,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"
          />
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              alignItems: "center",
            }}
          >
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
              }}
            >
              {this.gameStarted() &&
                <React.Fragment>
                  <button
                    style={{ background: "red" }}
                    onClick={() => {
                      axiosInstance.delete("/maps");
                    }}
                  >
                    Reset game
                  </button>
                  {this.getPlayer() &&
                    <React.Fragment>
                      <h2>Your info</h2>
                      <table
                        style={{
                          fontSize: 18,
                          position: "fixed",
                          top: 0,
                          width: "100%",
                          left: 0,
                          background:
                            "url(https://image.freepik.com/free-photo/old-wooden-texture-background-vintage_55716-1138.jpg)",
                        }}
                      >
                        <tbody>
                          <tr>
                            <td>Username</td>
                            <td>Energy</td>
                            <td>Money</td>
                            <td>Lifes</td>
                            <td>Score</td>
                          </tr>
                          <tr>
                            <td style={{ background: "#eedd9d", minWidth: 50 }}>
                              {this.getPlayer().userName}
                            </td>
                            <td style={{ background: "#eedd9d" }}>
                              {this.getPlayer().energy}
                            </td>
                            <td style={{ background: "#eedd9d" }}>
                              {this.getPlayer().money}
                            </td>
                            <td style={{ background: "#eedd9d" }}>
                              {this.getPlayer().lifeAmount}
                            </td>
                            <td style={{ background: "#eedd9d" }}>
                              {this.getPlayer().score}
                            </td>
                          </tr>
                        </tbody>
                      </table>
                      <pre>
                        {this.state.movingPlayer &&
                          this.getPlayer().username !==
                            this.state.movingPlayer &&
                          <React.Fragment>
                            Currently {this.state.movingPlayer} is in turn!
                          </React.Fragment>}
                      </pre>
                      {this.state.movingPlayer &&
                        this.getPlayer().username === this.state.movingPlayer &&
                        <h1>Your turn!</h1>}
                    </React.Fragment>}
                  {!this.getPlayer() &&
                    <React.Fragment>
                      <h2>
                        Game has started but you have not joined yet. Join now
                        by choosing your username below!
                      </h2>
                      <input
                        onChange={e =>
                          this.setState({ tempUsername: e.target.value })}
                        value={this.state.tempUsername}
                        type="text"
                      />
                      <button
                        onClick={() => {
                          axiosInstance
                            .post("/players", {
                              Username: this.state.tempUsername,
                            })
                            .then(res =>
                              this.setState({
                                username: this.state.tempUsername,
                              })
                            )
                            .catch(res =>
                              this.setState({
                                username: this.state.tempUsername,
                              })
                            );
                        }}
                      >
                        Join game
                      </button>
                    </React.Fragment>}
                  <h2>Map</h2>
                  <canvas
                    ref="canvas"
                    width={this.state.gridCells * this.state.gridCellSize}
                    height={this.state.gridCells2 * this.state.gridCellSize}
                    onClick={e => this.handleCanvasClick(e)}
                    onMouseMove={e => this.handleMouseMove(e)}
                  />
                </React.Fragment>}

              {!this.gameStarted() &&
                <React.Fragment>
                  <h2>
                    Happy holiday everyone!
                    <br/>
                    12.24 - 12.27 Rock +15%, Tree +12%, Water +8%
                    <br/>
                    12.31 - 01.02 Rock +10%, Tree +8%, Water +5%
                    <br/>
                    Game has not started yet. Be first and start your game
                    below!
                  </h2>
                  <StartGame
                    defaultX={this.state.gridCells}
                    defaultY={this.state.gridCells2}
                    setX={x => this.setState({ ...this.state, gridCells: x })}
                    setY={y => this.setState({ ...this.state, gridCells2: y })}
                    map={this.map}
                    updateCanvas={() => this.updateCanvas()}
                    setUsername={value =>
                      this.setState({ ...this.state, username: value })}
                    setPlayers={players =>
                      this.setState({ ...this.state, players })}
                  />
                </React.Fragment>}
            </div>
            {this.gameStarted() &&
              <div
                style={{
                  background:
                    "url(https://cdn7.dissolve.com/p/D2115_158_005/D2115_158_005_1200.jpg)",
                }}
              >
                <div>
                  <span style={{ fontSize: 28, textAlign: "center" }}>
                    Item shop
                  </span>
                  <table>
                    <tbody>
                      {this.state.items.map(e =>
                        <tr key={e.id}>
                          <td>
                            <img height="40px" src={e.itemPhotoSrc} />
                          </td>
                          <td>
                            {e.name}
                          </td>
                          <td>
                            {e.price}
                          </td>
                          <td>Buy</td>
                        </tr>
                      )}
                    </tbody>
                  </table>
                  <span
                    style={{
                      paddingTop: 60,
                      fontSize: 28,
                      textAlign: "center",
                    }}
                  >
                    Inventory
                  </span>
                  <table>
                    <tbody>
                      {this.getPlayer() &&
                        this.getPlayer().playerItems &&
                        this.getPlayer().playerItems.map(e =>
                          <tr key={e.id}>
                            <td>
                              <img height="40px" src={e.itemPhotoSrc} />
                            </td>
                            <td>
                              {e.name}
                            </td>
                            <td>
                              {e.price}
                            </td>
                            <td>Buy</td>
                          </tr>
                        )}
                    </tbody>
                  </table>
                </div>
              </div>}
          </div>
        </div>
      </div>
    );
  }
}
