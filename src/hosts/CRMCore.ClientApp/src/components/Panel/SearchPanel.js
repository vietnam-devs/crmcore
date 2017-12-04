import React, { Component } from 'react';
import { Card, CardHeader, CardBody, Collapse } from 'reactstrap';
import classnames from 'classnames';

export default class SearchPanel extends Component {
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = { collapse: false };
  }

  toggle() {
    this.setState({ collapse: !this.state.collapse });
  }

  render() {
    return (
      <Card className="b-panel">
        <CardHeader onClick={this.toggle}>
          <h3 className="b-panel-title">
            <i className="icon-magic-wand b-icon" />
            {this.props.panelTitle || 'Search parameters'}
          </h3>

          <span className="b-panel-actions">
            <i
              className={classnames(
                {
                  'icon-arrow-down': this.state.collapse === false
                },
                {
                  'icon-arrow-up': this.state.collapse === true
                },
                'b-icon'
              )}
            />
          </span>
        </CardHeader>

        <Collapse isOpen={this.state.collapse}>
          <CardBody>{this.props.children}</CardBody>
        </Collapse>
      </Card>
    );
  }
}
