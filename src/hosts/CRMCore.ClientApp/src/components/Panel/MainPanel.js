import React, { Component } from 'react';
import {
  Card,
  CardHeader,
  CardBody,
  ButtonDropdown,
  DropdownToggle
} from 'reactstrap';
// import classnames from 'classnames';

export default class MainPanel extends Component {
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = {
      dropdownOpen: false
    };
  }

  toggle() {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  }

  render() {
    return (
      <div className="animated fadeIn">
        {this.props.searchPanel || ''}
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-notebook b-icon" />
              {this.props.title || 'No title'}
            </h3>

            {this.props.actions && (
              <span className="b-panel-actions">
                <ButtonDropdown
                  isOpen={this.state.dropdownOpen}
                  toggle={this.toggle}
                >
                  <DropdownToggle caret color="primary" size="sm">
                    Actions
                  </DropdownToggle>

                  {this.props.actions || ''}
                </ButtonDropdown>
              </span>
            )}
          </CardHeader>

          <CardBody className="card-body">{this.props.children}</CardBody>
        </Card>
      </div>
    );
  }
}
